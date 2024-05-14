using RPGManager.Models;

namespace RPGManager.Validators
{


    /// <summary>
    /// zwraca validatorResult
    /// </summary>
    public class NPCValidator : IValidator <NPC>
    {
        public Validator Validate(NPC npc)
        {
            Validator validator = new Validator();
            validator.IsValid = true;
            validator.Message = "ok";

            if (npc.Name == null || npc.Name.Length < 2)
            {
                validator.IsValid = false;
                validator.Message = "Brak imienia lub imie jest za krótkie (mniej niż co najmniej dwa znaki";
                return validator;
            }
            if (npc.Exp < 0)
            {
                validator.IsValid = false;
                validator.Message = "brak wprowadzonej lub błędna wartość exp. Exp nie może być mniejsze niż 0";
                return validator;
            }
            if (npc.Strength < 0)
            {
                validator.IsValid = false;
                validator.Message = "brak wprowadzonej lub błędna wartość siła. Siłą nie może być mniejsze niż 0";
                return validator;
            }
            if (npc.Might < 0)
            {
                validator.IsValid = false;
                validator.Message = "brak wprowadzonej lub błędna wartość coc. Moc nie może być mniejsze niż 0";
                return validator;
            }

            return validator;
        }

    }
}
