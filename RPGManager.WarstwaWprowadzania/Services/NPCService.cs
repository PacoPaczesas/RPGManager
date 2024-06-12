using Microsoft.EntityFrameworkCore;
//using NowaKlasa = RPGManager.NowaKlasa2;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using RPGManager.WarstwaWprowadzania.Data;
using RPGManager.WarstwaDomenowa.Models;
using RPGManager.WarstwaWprowadzania.Dtos;
using RPGManager.WarstwaWprowadzania.Services.Interfaces;
using RPGManager.WarstwaWprowadzania.Validators;

// namespace nie potrzebuje klamerek kiedy jest jeden dla całego pliku
namespace RPGManager.WarstwaWprowadzania.Services;

public class NPCService : INPCService
{
    private readonly IDataContext _context;
    private readonly IValidator<NPC> _NPCValidator;

    public NPCService(IDataContext context, IValidator<NPC> NPCValidator)
    {
        _context = context;
        _NPCValidator = NPCValidator;
    }

    public async Task<IEnumerable<NPC>> GetNPCs()
    {
        var npcs = await _context.NPCs
            .Include(npc => npc.Country)
            .OrderBy(npc => npc.Id)
            .ToListAsync();
        return npcs;
    }

    public async Task<NPC> GetNPC(int id)
    {
        var npc = await _context.NPCs
            .Include(npc => npc.Country)
            .Include(npc => npc.Notes)
            .FirstOrDefaultAsync(npc => npc.Id == id);
        return npc;
    }

    public async Task<ValidatorResult<NPC>> AddNPC(NPCDto npcDto)
    {
        ValidatorResult<NPC> NPCvalidator = new ValidatorResult<NPC>();

        var npc = new NPC(npcDto.Exp, npcDto.Strength, npcDto.Might)
        {
            Name = npcDto.Name,
            Description = npcDto.Description,
            CountryId = npcDto.CountryId,
        };

        NPCvalidator = _NPCValidator.Validate(npc);

        if (NPCvalidator.IsCompleate)
        {
            await _context.NPCs.AddAsync(npc);
            await _context.SaveChangesAsync();

            return NPCvalidator;
        }
        return NPCvalidator;
    }

    public async Task<ValidatorResult<NPC>> UpdateNPC(int id, NPCDto npcDto)
    {
        ValidatorResult<NPC> NPCvalidator = new ValidatorResult<NPC>();

        var npc = await _context.NPCs.FindAsync(id);

        if (npc == null)
        {
            return null;
        }

        npc.Name = npcDto.Name;
        npc.Description = npcDto.Description;
        npc.CountryId = npcDto.CountryId;

        NPCvalidator = _NPCValidator.Validate(npc);

        if (NPCvalidator.IsCompleate)
        {
            _context.NPCs.Update(npc);
            await _context.SaveChangesAsync();
            return NPCvalidator;
        }

        return NPCvalidator;
    }

    public async Task<NPC> DeleteNPC(int id)
    {
        var npc = await _context.NPCs.FindAsync(id);
        if (npc == null)
        {
            return null;
        }

        _context.NPCs.Remove(npc);
        await _context.SaveChangesAsync();

        return npc;
    }

    public async Task<ValidatorResult<NPC>> Attack(int attackerId, int defenderId)
    {
        ValidatorResult<NPC> AttackValidator = new ValidatorResult<NPC>
        {
            IsCompleate = true,
            Message = "ok",
            obj = null
        };

        var attacker = await _context.NPCs.FindAsync(attackerId);
        var defender = await _context.NPCs.FindAsync(defenderId);

        if (attacker == null || defender == null)
        {
            AttackValidator.IsCompleate = false;
            AttackValidator.Message = "Wprowadzono błędne ID. Co najmniej jeden z NPC o wprowadzonych ID nie istnieje";
            return AttackValidator;
        }

        int attackPower = attacker.AttackPower();
        if (attackPower > defender.AC)
        {
            defender.minusHp(attackPower);
            attacker.addExp(5);
            AttackValidator.Message = "Sukces. Atakujący wykonał atak i zadał obrażenia";

            _context.NPCs.Update(attacker);
            await _context.SaveChangesAsync();
            return AttackValidator;
        }

        AttackValidator.Message = "Atak się nie udał";
        return AttackValidator;
    }


}

