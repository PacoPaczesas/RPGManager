using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RPGManager.Data;
using RPGManager.Models;
using System.ComponentModel.DataAnnotations;

namespace RPGManager.Validators
{
    public class NoteValidator : IValidator<Note>
    {

        private readonly DataContext _context;
        public NoteValidator(DataContext context)
        {
            _context = context;
        }

        public ValidatorResult<Note> Validate (Note note)
        {
            ValidatorResult<Note> validator = new ValidatorResult<Note>();
            validator.IsCompleate = true;
            validator.Message = "ok";
            validator.obj = note;

            if (note.Title.Length < 1)
            {
                validator.IsCompleate = false;
                validator.Message = "Brak tytułu notatki";
                return validator;
            }

            if (note.Text.Length < 1)
            {
                validator.IsCompleate = false;
                validator.Message = "Brak treści notatki";
                return validator;
            }

            if (!_context.NPCs.Any(npc => npc.Id == note.NPCId))
            {
                validator.IsCompleate = false;
                validator.Message = "NPC o podanym ID nie istnieje";
                return validator;
            }

            return validator;
        }


    }


}
