using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RPGManager.WarstwaDomenowa.Models;
using RPGManager.WarstwaWprowadzania.Dtos;
using RPGManager.WarstwaWprowadzania.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

// OKOK

namespace RPGManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "GM")]
    public class NotesController : ControllerBase
    {
        private readonly INoteService _noteService;

        public NotesController(INoteService noteService)
        {
            _noteService = noteService;
        }

        //adres GET: api/Notes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Note>> GetNoteById(int id)
        {
            var note = await _noteService.GetNoteAsync(id);

            if (note == null)
            {
                return NotFound("Notataka o danym Id nie istnieje");
            }

            return Ok(note);
        }

        //adres POST: api/Notes
        [HttpPost]
        public async Task<ActionResult<Result<Note>>> CreateNote([FromBody] NoteDto noteDto)
        {
            var noteValidator = await _noteService.AddNoteAsync(noteDto);

            if (!noteValidator.IsSuccessful)
            {
                return BadRequest(noteValidator.Message);
            }

            return CreatedAtAction(nameof(GetNoteById), new { id = noteValidator.obj.Id }, noteValidator.obj);
        }

        // adres PUT: api/Notes/id
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateNote(int id, [FromBody] NoteDto noteDto)
        {
            var noteValidator = await _noteService.UpdateNoteAsync(id, noteDto);
            if (!noteValidator.IsSuccessful)
            {
                return BadRequest(noteValidator.Message);
            }
            return Ok("Zapisano zmiany");
        }

        //adres DELETE: api/Notes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(int id)
        {
            var note = await _noteService.DeleteNoteAsync(id);
            if (note == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
