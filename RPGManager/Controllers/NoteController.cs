using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RPGManager.Data;
using RPGManager.Dtos;
using RPGManager.Models;
using RPGManager.Services.Interfaces;
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
                return NotFound();
            }

            return Ok(note);
        }

        //adres POST: api/Notes
        [HttpPost]
        //public ActionResult<(Note, Validator)> CreateNote([FromBody] NoteDto noteDto)
        public ActionResult <ValidatorResult<Note>> CreateNote([FromBody] NoteDto noteDto)
        {
            ValidatorResult<Note> NoteValidator = new ValidatorResult<Note>();
            NoteValidator = _noteService.AddNote(noteDto);

            //var result = _noteService.AddNote(noteDto);

            if (!NoteValidator.IsCompleate)
            {
                return BadRequest(NoteValidator.Message);
            }
 
            return CreatedAtAction(nameof(GetNoteById), new { id = NoteValidator.obj.Id }, NoteValidator.obj);
        }

        // adres PUT: api/Notes/id
        [HttpPut("{id}")]
        public ActionResult UpdateNote(int id, [FromBody] NoteDto noteDto)
        {
            var note = _noteService.UpdateNote(id, noteDto);
            if (note == null)
            {
                return NotFound();
            }
            return NoContent(); // Zwraca status 204 No Content po pomyślnej aktualizacji
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
