
using RPGManager.Dtos;
using RPGManager.Models;
using RPGManager.Services.Interfaces;
using RPGManager.Validators;
using RPGManager.WarstwaWprowadzania.Data;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace RPGManager.Services
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

        public ValidatorResult<Note> AddNote(NoteDto noteDto)
        {
            ValidatorResult<Note> NoteValidator = new ValidatorResult<Note>();

            var note = new Note
            {
                Title = noteDto.Title,
                Text = noteDto.Text,
                NPCId = noteDto.NPCId
            };

            NoteValidator = _NoteValidator.Validate(note);
            if(NoteValidator.IsCompleate)
            {
                _context.Notes.Add(note);
                _context.SaveChanges();
                return (NoteValidator);
            }

            return (NoteValidator);
        }

        public ValidatorResult<Note> UpdateNote (int id, NoteDto noteDto)
        {
            ValidatorResult<Note> NoteValidator = new ValidatorResult<Note>();
            var note = _context.Notes.Find(id);

            if (note == null)
            {
                NoteValidator.IsCompleate = false;
                NoteValidator.Message = "Nie znaleziono notatki o wskazanym Id";
                return NoteValidator;
            }

            note.Title = noteDto.Title;
            note.Text = noteDto.Text;
            note.NPCId = noteDto.NPCId;

            NoteValidator = _NoteValidator.Validate(note);

            if (!NoteValidator.IsCompleate)
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
