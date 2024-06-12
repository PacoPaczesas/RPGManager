using RPGManager.WarstwaDomenowa.Models;
using System.ComponentModel.DataAnnotations;

namespace RPGManager.WarstwaWprowadzania.Validators
{


    /// <summary>
    /// zwraca validatorResult
    /// </summary>
    public class NPCValidator : IValidator<NPC>
    {
        public Result<NPC> Validate(NPC npc)
        {
            Result<NPC> NPCValidator = new Result<NPC>();
            NPCValidator.IsSuccessful = true;
            NPCValidator.Message = "ok";
            NPCValidator.obj = npc;

            if (npc.Name == null || npc.Name.Length < 2)
            {
                NPCValidator.IsSuccessful = false;
                NPCValidator.Message = "Brak imienia lub imie jest za krótkie (mniej niż dwa znaki";
                return NPCValidator;
            }
            if (npc.Exp < 0)
            {
                NPCValidator.IsSuccessful = false;
                NPCValidator.Message = "brak wprowadzonej lub błędna wartość exp. Exp nie może być mniejsze niż 0";
                return NPCValidator;
            }
            if (npc.Strength < 1)
            {
                NPCValidator.IsSuccessful = false;
                NPCValidator.Message = "brak wprowadzonej lub błędna wartość siła. Siłą nie może być mniejsze niż 1";
                return NPCValidator;
            }
            if (npc.Might < 1)
            {
                NPCValidator.IsSuccessful = false;
                NPCValidator.Message = "brak wprowadzonej lub błędna wartość coc. Moc nie może być mniejsze niż 1";
                return NPCValidator;
            }

            return NPCValidator;
        }

    }
}
