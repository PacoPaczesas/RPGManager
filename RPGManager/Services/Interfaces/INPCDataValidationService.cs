using RPGManager.Dtos;

namespace RPGManager.Services.Interfaces
{
    public interface INPCDataValidationService
    {
        bool NPCDataValidation(NPCDto npcDto);
        int NPCLvlAssign (NPCDto npcDto);
    }
}
