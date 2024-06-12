using RPGManager.WarstwaDomenowa.Models;
using RPGManager.WarstwaWprowadzania.Data;
using RPGManager.WarstwaWprowadzania.Dtos;
using RPGManager.WarstwaWprowadzania.Services.Interfaces;
using RPGManager.WarstwaWprowadzania.Validators;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace RPGManager.WarstwaWprowadzania.Services
{
    public class NoteService : INoteService
    {
        private readonly IDataContext _context;
        private readonly IValidator<Note> _NoteValidator;

        public NoteService(IDataContext context, IValidator<Note> noteValidator)
        {
            _context = context;
            _NoteValidator = noteValidator;
        }

        public Note GetNote(int id)
        {
            var note = _context.Notes.Find(id);
            if (note == null)
            {
                return null;
            }

            return note;
        }

        public Result<Note> AddNote(NoteDto noteDto)
        {
            // Result<Note> NoteValidator = new Result<Note>();

            var note = new Note
            {
                Title = noteDto.Title,
                Text = noteDto.Text,
                NPCId = noteDto.NPCId
            };

            var NoteValidator = _NoteValidator.Validate(note);
            if (NoteValidator.IsSuccessful)
            {
                _context.Notes.Add(note);
                _context.SaveChanges();
                return NoteValidator;
            }

            return NoteValidator;
        }

        public Result<Note> UpdateNote(int id, NoteDto noteDto)
        {
            Result<Note> NoteValidator = new Result<Note>();
            var note = _context.Notes.Find(id);

            if (note == null)
            {
                NoteValidator.IsSuccessful = false;
                NoteValidator.Message = "Nie znaleziono notatki o wskazanym Id";
                return NoteValidator;
            }

            note.Title = noteDto.Title;
            note.Text = noteDto.Text;
            note.NPCId = noteDto.NPCId;

            NoteValidator = _NoteValidator.Validate(note);

            if (!NoteValidator.IsSuccessful)
            {
                return NoteValidator;
            }

            _context.Notes.Update(note);
            _context.SaveChanges();

            return NoteValidator;
        }

        public Note DeleteNote(int id)
        {
            var note = _context.Notes.Find(id);
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
