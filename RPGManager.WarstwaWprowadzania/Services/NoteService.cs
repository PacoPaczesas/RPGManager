using Microsoft.EntityFrameworkCore;
using RPGManager.WarstwaDomenowa.Models;
using RPGManager.WarstwaWprowadzania.Data;
using RPGManager.WarstwaWprowadzania.Dtos;
using RPGManager.WarstwaWprowadzania.Services.Interfaces;
using RPGManager.WarstwaWprowadzania.Validators;
using System.Threading.Tasks;

namespace RPGManager.WarstwaWprowadzania.Services
{
    public class NoteService : INoteService
    {
        private readonly IDataContext _context;
        private readonly IValidator<Note> _noteValidator;

        public NoteService(IDataContext context, IValidator<Note> noteValidator)
        {
            _context = context;
            _noteValidator = noteValidator;
        }

        public async Task<Note> GetNoteAsync(int id)
        {
            return await _context.Notes
                .Include(n => n.NPC)
                .FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<Result<Note>> AddNoteAsync(NoteDto noteDto)
        {
            var note = new Note
            {
                Title = noteDto.Title,
                Text = noteDto.Text,
                NPCId = noteDto.NPCId
            };

            var noteValidator = _noteValidator.Validate(note);
            if (noteValidator.IsSuccessful)
            {
                await _context.Notes.AddAsync(note);
                _context.SaveChanges();
                return noteValidator;
            }

            return noteValidator;
        }

        public async Task<Result<Note>> UpdateNoteAsync(int id, NoteDto noteDto)
        {
            var noteValidator = new Result<Note>();
            var note = await _context.Notes.FindAsync(id);

            if (note == null)
            {
                noteValidator.IsSuccessful = false;
                noteValidator.Message = "Nie znaleziono notatki o wskazanym Id";
                return noteValidator;
            }

            note.Title = noteDto.Title;
            note.Text = noteDto.Text;
            note.NPCId = noteDto.NPCId;

            noteValidator = _noteValidator.Validate(note);

            if (!noteValidator.IsSuccessful)
            {
                return noteValidator;
            }

            _context.Notes.Update(note);
            _context.SaveChanges();
            return noteValidator;
        }

        public async Task<Note> DeleteNoteAsync(int id)
        {
            var note = await _context.Notes.FindAsync(id);
            if (note == null)
            {
                return null;
            }

            _context.Notes.Remove(note);
            _context.SaveChanges();
            return note;
        }
    }
}
