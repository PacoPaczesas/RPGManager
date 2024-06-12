using RPGManager.WarstwaDomenowa.Models;
using RPGManager.WarstwaWprowadzania.Data;


namespace RPGManager.WarstwaWprowadzania.Validators
{
    public class NoteValidator : IValidator<Note>
    {
        private readonly IDataContext _context;
        public NoteValidator(IDataContext context)
        {
            _context = context;
        }

        public Result<Note> Validate(Note note)
        {
            Result<Note> validator = new Result<Note>();
            validator.IsSuccessful = true;
            validator.Message = "ok";
            validator.obj = note;

            if (note.Title.Length < 1)
            {
                validator.IsSuccessful = false;
                validator.Message = "Brak tytułu notatki";
                return validator;
            }

            if (note.Text.Length < 1)
            {
                validator.IsSuccessful = false;
                validator.Message = "Brak treści notatki";
                return validator;
            }

            if (!_context.NPCs.Any(npc => npc.Id == note.NPCId))
            {
                validator.IsSuccessful = false;
                validator.Message = "NPC o podanym ID nie istnieje";
                return validator;
            }

            return validator;
        }


    }


}
