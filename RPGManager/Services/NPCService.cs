﻿using Microsoft.EntityFrameworkCore;
using RPGManager.Data;
using RPGManager.Dtos;
using RPGManager.Models;
using RPGManager.Services.Interfaces;
//using NowaKlasa = RPGManager.NowaKlasa2;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace RPGManager.Services
{
    public class NPCService : INPCService
    {
        private readonly DataContext _context;


        public NPCService(DataContext context)
        {
            _context = context;

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

        public (NPC, Validator) AddNPC(NPCDto npcDto)
        {
            Validator NPCvalidator = new Validator();

            var npc = new NPC(npcDto.Exp, npcDto.Strength, npcDto.Might)
            {
                Name = npcDto.Name,
                Description = npcDto.Description,
                CountryId = npcDto.CountryId,
            };

            NPCvalidator = npc.Validate();

            if (NPCvalidator.IsValid)
            {         
                _context.NPCs.Add(npc);
                _context.SaveChanges();

                return (npc, null);
            }
            return (null, NPCvalidator);
        }

        public NPC UpdateNPC(int id, NPCDto npcDto)
        {
            var npc = _context.NPCs.Find(id);

            if (npc == null)
            {
                return null;
            }


            npc.Name = npcDto.Name;
            npc.Description = npcDto.Description;
            npc.CountryId = npcDto.CountryId;

            _context.NPCs.Update(npc);
            _context.SaveChanges();

            return npc;
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



    }
}