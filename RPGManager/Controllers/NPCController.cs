using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RPGManager.Data;
using RPGManager.Dtos;
using RPGManager.Models;

namespace RPGManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NPCsController : ControllerBase
    {
        private readonly DataContext _context;

        public NPCsController(DataContext context)
        {
            _context = context;
        }

        //adres GET: api/NPCs
        [HttpGet]
        public ActionResult<IEnumerable<NPC>> GetNPCs()
        {
            var npcs = _context.NPCs
                .Include(npc => npc.Country)
                //.Include(npc => npc.Notes) TODO nie działa zwracanie przypisanych notatek
                .OrderBy(npc => npc.Id).ToList();
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
            var npc = _context.NPCs
                .Include(npc => npc.Country)
                //.Include(npc => npc.Notes)
                .FirstOrDefault(npc => npc.Id == id);
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
            var npc = new NPC
            {
                Name = npcDto.Name,
                Description = npcDto.Description,
                CountryId = npcDto.CountryId
            };

            _context.NPCs.Add(npc);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetNPC), new { id = npc.Id }, npc);
        }

        //adres DELETE: api/NPCs/id
        [HttpDelete("{id}")]
        public IActionResult DeleteNPC(int id)
        {
            var npc = _context.NPCs.Find(id);
            if (npc == null)
            {
                return NotFound();
            }

            _context.NPCs.Remove(npc);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
