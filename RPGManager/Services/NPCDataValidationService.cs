using RPGManager.Dtos;
using RPGManager.Services.Interfaces;

namespace RPGManager.Services
{
    public class NPCDataValidationService : INPCDataValidationService
    {
        public bool NPCDataValidation(NPCDto npcDto)
        {
            bool isCorrect = true;

            if (npcDto.Name == null || npcDto.Name.Length < 2)
            {
                isCorrect = false;
            }

            if (npcDto.Strength < 1 || npcDto.Might < 1 || npcDto.HP < 0 || npcDto.Exp < 0)
            {
                isCorrect = false;
            }

            return isCorrect;
        }

        public int NPCLvlAssign (NPCDto npcDto)
        {
            if (npcDto.Exp < 100)
            {
                return 1;
            }
            else if (npcDto.Exp < 300)
            {
                return 2;
            }
            else if (npcDto.Exp < 900)
            {
                return 3;
            }
            else
            {
                return 4;
            }
        }










    }
}
