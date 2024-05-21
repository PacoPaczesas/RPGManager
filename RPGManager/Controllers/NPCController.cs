using Microsoft.AspNetCore.Mvc;
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
        public ActionResult <ValidatorResult<NPC>> PostNPC([FromBody] NPCDto npcDto)
        {
            ValidatorResult<NPC> NPCValidator = new ValidatorResult<NPC>();
            NPCValidator = _npcService.AddNPC(npcDto);

            //var result = _npcService.AddNPC(npcDto);

            if (!NPCValidator.IsCompleate)
            {
                return BadRequest(NPCValidator.Message);
            }
            return CreatedAtAction(nameof(GetNPC), new { id = NPCValidator.obj.Id}, NPCValidator.obj);
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

        //atak
        [HttpPost("attack/{attackerId}/{defenderId}")]
        public IActionResult Attack(int attackerId, int defenderId)
        {
            // na początku sprawdzam czy dane NPC wogóle istnieją. Zastanawiam się czy powinno to mieć miejsce tutaj czy w NPCService. Przeniosłem to do NPC service
            /*            var attacker = _npcService.GetNPC(attackerId);
                        var defender = _npcService.GetNPC(defenderId);

                        if (attacker == null || defender == null)
                        {
                            return NotFound("Nie znaleziono NPC o danych ID");
                        }*/


            ValidatorResult<NPC> AttackValidator = new ValidatorResult<NPC>();
            AttackValidator = _npcService.Attack(attackerId, defenderId);

            if (!AttackValidator.IsCompleate)
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}
