using RPGManager.Data;
using RPGManager.Dtos;
using RPGManager.Models;
using RPGManager.Services.Interfaces;
using RPGManager.Validators;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace RPGManager.Services
{
    public class NoteService : INoteService
    {
        private readonly DataContext _context;
        private readonly IValidator<Note> _NoteValidator;

        public NoteService(DataContext context, IValidator<Note> noteValidator)
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

        public Note UpdateNote (int id, NoteDto noteDto)
        {
            var note = _context.Notes.Find(id);

            if (note == null)
            {
                return null;
            }
            note.Title = noteDto.Title;
            note.Text = noteDto.Text;
            note.NPCId = noteDto.NPCId;

            _context.Notes.Update(note);
            _context.SaveChanges(); 
            
            return note;
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
