﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RPGManager.Data;
using RPGManager.Dtos;
using RPGManager.Models;
using RPGManager.Services.Interfaces;

namespace RPGManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NPCsController : ControllerBase
    {
        private readonly INPCService _npcService;

        public NPCsController (INPCService npcService)
        {
            _npcService = npcService;
        }

        //adres GET: api/NPCs
        [HttpGet]
        public ActionResult<IEnumerable<NPC>> GetNPCs()
        {
            var npcs = _npcService.GetNPCs();
            if (npcs == null)
            {
                return NotFound(); // Zwraca błąd 404 jeżeli lista Country jest pusta
            }
            return Ok(npcs);
        }

        //adres GET: api/NPCs/id
        [HttpGet("{id}")]
        public ActionResult<NPC> GetNPC(int id)
        {
            var npc = _npcService.GetNPC(id);
            if (npc == null)
            {
                return NotFound();
            }
            return npc;
        }

        //adres POST: api/NPCs
        [HttpPost]
        public ActionResult<NPC> PostNPC([FromBody] NPCDto npcDto)
        {
            var npc = _npcService.AddNPC(npcDto);
            if (npc == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetNPC), new { id = npc.Id }, npc);
        }

        // adres PUT: api/NPC/id
        [HttpPut("{id}")]
        public ActionResult UpdateNpc(int id, [FromBody] NPCDto npcDto)
        {
            var npc = _npcService.UpdateNPC(id, npcDto);

            if (npc == null)
            {
                return NotFound();
            }
            return NoContent(); // Zwraca status 204 No Content po pomyślnej aktualizacji
        }

        //adres DELETE: api/NPCs/id
        [HttpDelete("{id}")]
        public IActionResult DeleteNPC(int id)
        {
            var npc = _npcService.DeleteNPC(id);
            if (npc == null)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}
