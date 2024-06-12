using Microsoft.EntityFrameworkCore;
//using NowaKlasa = RPGManager.NowaKlasa2;
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

    public async Task<IEnumerable<NPC>> GetNPCs(CancellationToken token)
    {
        var npcs = await _context.NPCs
            .Include(npc => npc.Country)
            .OrderBy(npc => npc.Id)
            .ToListAsync(token);
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

    public async Task<Result<NPC>> AddNPC(NPCDto npcDto)
    {
        //Result<NPC> NPCvalidator = new Result<NPC>();

        var npc = new NPC(npcDto.Exp, npcDto.Strength, npcDto.Might)
        {
            Name = npcDto.Name,
            Description = npcDto.Description,
            CountryId = npcDto.CountryId,
        };

        var NPCvalidator = _NPCValidator.Validate(npc);

        if (NPCvalidator.IsSuccessful)
        {
            await _context.NPCs.AddAsync(npc);
            // await _context.SaveChangesAsync();
            _context.SaveChanges();


            return NPCvalidator;
        }
        return NPCvalidator;
    }

    public async Task<Result<NPC>> UpdateNPC(int id, NPCDto npcDto)
    {
        //Result<NPC> NPCvalidator = new Result<NPC>();

        var npc = await _context.NPCs.FindAsync(id);

        if (npc == null)
        {
            return null;
        }

        npc.Name = npcDto.Name;
        npc.Description = npcDto.Description;
        npc.CountryId = npcDto.CountryId;

        var NPCvalidator = _NPCValidator.Validate(npc);

        if (NPCvalidator.IsSuccessful)
        {
            _context.NPCs.Update(npc);
            //await _context.SaveChangesAsync();
            _context.SaveChanges();
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
        //await _context.SaveChangesAsync();
        _context.SaveChanges();

        return npc;
    }

    public async Task<Result<NPC>> Attack(int attackerId, int defenderId)
    {
        Result<NPC> AttackValidator = new Result<NPC>
        {
            IsSuccessful = true,
            Message = "ok",
            obj = null
        };

        // oba mają wykonać się synchronicznie
        var attacker = await _context.NPCs.FindAsync(attackerId);
        var defender = await _context.NPCs.FindAsync(defenderId);
        // >> następnie czekamy aż oba się wykonają i gdy będą skończone idziemy dalej


        if (attacker == null || defender == null)
        {
            AttackValidator.IsSuccessful = false;
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
            //await _context.SaveChangesAsync();
            _context.SaveChanges();
            return AttackValidator;
        }

        AttackValidator.Message = "Atak się nie udał";
        return AttackValidator;
    }


}

