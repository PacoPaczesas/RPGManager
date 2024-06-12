using Microsoft.AspNetCore.Mvc;
using RPGManager.WarstwaDomenowa.Models;
using RPGManager.WarstwaWprowadzania.Dtos;
using RPGManager.WarstwaWprowadzania.Services.Interfaces;

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
        public async Task<ActionResult<IEnumerable<NPC>>> GetNPCs()
        {
            var npcs = await _npcService.GetNPCs();
            if (npcs == null || !npcs.Any())
            {
                return NotFound("Lista NPC jest pusta");
            }
            return Ok(npcs);
        }

        //adres GET: api/NPC/id
        [HttpGet("{id}")]
        public async Task<ActionResult<NPC>> GetNPC(int id)
        {
            var npc = await _npcService.GetNPC(id);
            if (npc == null)
            {
                return BadRequest("NPC o danym Id nie istnieje");
            }
            return Ok(npc);
        }

        //adres POST: api/NPCs
        [HttpPost]
        public async Task<ActionResult<ValidatorResult<NPC>>> PostNPC([FromBody] NPCDto npcDto)
        {
            var NPCValidator = await _npcService.AddNPC(npcDto);

            if (!NPCValidator.IsCompleate)
            {
                return BadRequest(NPCValidator.Message);
            }
            return CreatedAtAction(nameof(GetNPC), new { id = NPCValidator.obj.Id }, NPCValidator.obj);
        }

        // adres PUT: api/NPC/id
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateNpc(int id, [FromBody] NPCDto npcDto)
        {
            var NPCValidator = await _npcService.UpdateNPC(id, npcDto);

            if (!NPCValidator.IsCompleate)
            {
                return BadRequest(NPCValidator.Message);
            }
            return Ok("Zaktualizowano dane");
        }

        //adres DELETE: api/NPCs/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNPC(int id)
        {
            var npc = await _npcService.DeleteNPC(id);
            if (npc == null)
            {
                return NotFound();
            }

            return Ok("Usunięto NPC");
        }

        //atak
        [HttpPost("attack/{attackerId}/{defenderId}")]
        public async Task<IActionResult> Attack(int attackerId, int defenderId)
        {
            var AttackValidator = await _npcService.Attack(attackerId, defenderId);

            if (!AttackValidator.IsCompleate)
            {
                return BadRequest(AttackValidator.Message);
            }
            return Ok(AttackValidator.Message);
        }

    }
}
