using Microsoft.AspNetCore.Mvc;
using RPGManager.Models;

namespace RPGManager.Validators
{
    public class NoteValidator : IValidator<Note>
    {
        public Validator Validate (Note note)
        {
            Validator validator = new Validator();
            validator.IsValid = true;
            validator.Message = "ok";

            if (note.Title.Length < 1)
            {
                validator.IsValid = false;
                validator.Message = "Brak tytułu notatki";
                return validator;
            }

            if (note.Text.Length < 1)
            {
                validator.IsValid = false;
                validator.Message = "Brak treści notatki";
                return validator;
            }

            return validator;
        }


    }


}
