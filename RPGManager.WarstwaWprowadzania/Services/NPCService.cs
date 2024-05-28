﻿using Microsoft.EntityFrameworkCore;
using RPGManager.Dtos;
using RPGManager.Models;
using RPGManager.Services.Interfaces;
//using NowaKlasa = RPGManager.NowaKlasa2;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using RPGManager.Validators;
using RPGManager.WarstwaWprowadzania.Data;

// namespace nie potrzebuje klamerek kiedy jest jeden dla całego pliku
namespace RPGManager.Services;

public class NPCService : INPCService
{
    private readonly IDataContext _context;
    private readonly IValidator<NPC> _NPCValidator;

    public NPCService(IDataContext context, IValidator<NPC> NPCValidator)
    {
        _context = context;
        _NPCValidator = NPCValidator;
    }

    public IEnumerable<NPC> GetNPCs()
    {
        var npcs = _context.NPCs
        .Include(npc => npc.Country)
        .OrderBy(npc => npc.Id).ToList();
        return npcs;
    }

    public NPC GetNPC(int id)
    {
        var npc = _context.NPCs
            .Include(npc => npc.Country)
            .Include(npc => npc.Notes)
            .FirstOrDefault(npc => npc.Id == id);
        return npc;
    }

    public ValidatorResult<NPC> AddNPC(NPCDto npcDto)
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
            _context.NPCs.Add(npc);
            _context.SaveChanges();

            return (NPCvalidator);
        }
        return (NPCvalidator);
    }

    public ValidatorResult<NPC> UpdateNPC(int id, NPCDto npcDto)

    {
        ValidatorResult<NPC> NPCvalidator = new ValidatorResult<NPC>();

        var npc = _context.NPCs.Find(id);

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
            _context.SaveChanges();
            return NPCvalidator;
        }

        return NPCvalidator;
    }

    public NPC DeleteNPC(int id)
    {
        var npc = _context.NPCs.Find(id);
        if (npc == null)
        {
            return null;
        }

        _context.NPCs.Remove(npc);
        _context.SaveChanges();

        return npc;
    }

    public ValidatorResult<NPC> Attack(int attackerId, int defenderId)
    {
        ValidatorResult<NPC> AttackValidator = new ValidatorResult<NPC>();
        AttackValidator.IsCompleate = true;
        AttackValidator.Message = "ok";
        AttackValidator.obj = null;

        var attacker = _context.NPCs.Find(attackerId);
        var defender = _context.NPCs.Find(defenderId);

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
            _context.SaveChanges();
            return AttackValidator;
        }

        AttackValidator.Message = "Atak się nie udał";
        return AttackValidator;
    }


}

