using Microsoft.AspNetCore.Mvc;
using RPGManager.WarstwaDomenowa.Models;
using RPGManager.WarstwaWprowadzania.Dtos;
using RPGManager.WarstwaWprowadzania.Services.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace RPGManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INoteService _noteService;

        public NotesController(INoteService noteService)
        {
            _noteService = noteService;
        }


        //adres GET: api/Notes/5
        [HttpGet("{id}")]
        public ActionResult<Note> GetNoteById(int id)
        {
            var note = _noteService.GetNote(id);

            if (note == null)
            {
                return NotFound("Notataka o danym Id nie istnieje");
            }

            return Ok(note);
        }

        //adres POST: api/Notes
        [HttpPost]
        public ActionResult <Result<Note>> CreateNote([FromBody] NoteDto noteDto)
        {
            // Result<Note> NoteValidator = new Result<Note>();
            var NoteValidator = _noteService.AddNote(noteDto);

            if (!NoteValidator.IsSuccessful)
            {
                return BadRequest(NoteValidator.Message);
            }
 
            return CreatedAtAction(nameof(GetNoteById), new { id = NoteValidator.obj.Id }, NoteValidator.obj);
        }

        // adres PUT: api/Notes/id
        [HttpPut("{id}")]
        public ActionResult UpdateNote(int id, [FromBody] NoteDto noteDto)
        {
            //Result<Note> NoteValidator = new Result<Note>();
            var NoteValidator = _noteService.UpdateNote(id, noteDto);
            if (!NoteValidator.IsSuccessful)
            {
                return BadRequest(NoteValidator.Message);
            }
            return Ok("Zapisano zmiany");
        }

        //adres DELETE: api/Notes/5
        [HttpDelete("{id}")]
        public IActionResult DeleteNote(int id)
        {
            var note = _noteService.DeleteNote(id);
            if (note == null)
            {
                return NotFound();
            }
            return NoContent();
        }


    }
}
