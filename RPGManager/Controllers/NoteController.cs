using Microsoft.AspNetCore.Mvc;
using RPGManager.Data;
using RPGManager.Dtos;
using RPGManager.Models;
using System.Linq;

namespace RPGManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly DataContext _context;

        public NotesController(DataContext context)
        {
            _context = context;
        }

        // tutaj nie mam wyświetlania wszystkich notatak gdyż chciałbym, aby były one wyświetlane przy odpowiednim NPC. Nad tym jeszcze pracuję.

        //adres GET: api/Notes/5
        [HttpGet("{id}")]
        public ActionResult<Note> GetNoteById(int id)
        {
            var note = _context.Notes.Find(id);

            if (note == null)
            {
                return NotFound();
            }

            return note;
        }

        //adres POST: api/Notes
        [HttpPost]
        public ActionResult<Note> CreateNote([FromBody] NoteDto noteDto)
        {
            var note = new Note
            {
                Title = noteDto.Title,
                Text = noteDto.Text,
                NPCId = noteDto.NPCId
            };

            _context.Notes.Add(note);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetNoteById), new { id = note.Id }, note);
        }

        // adres PUT: api/Notes/id
        [HttpPut("{id}")]
        public ActionResult UpdateNote(int id, [FromBody] NoteDto noteDto)
        {
            var note = _context.Notes.Find(id);

            if (note == null)
            {
                return NotFound();
            }
            note.Title = noteDto.Title;
            note.Text = noteDto.Text;
            note.NPCId = noteDto.NPCId;

            _context.Notes.Update(note);
            _context.SaveChanges();

            return NoContent(); // Zwraca status 204 No Content po pomyślnej aktualizacji
        }

        //adres DELETE: api/Notes/5
        [HttpDelete("{id}")]
        public IActionResult DeleteNote(int id)
        {
            var note = _context.Notes.Find(id);
            if (note == null)
            {
                return NotFound();
            }

            _context.Notes.Remove(note);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
