using RPGManager.WarstwaDomenowa.Models;
using RPGManager.WarstwaWprowadzania.Dtos;

namespace RPGManager.WarstwaWprowadzania.Services.Interfaces
{
    public interface INPCService
    {
        Task<IEnumerable<NPC>> GetNPCs();
        Task<NPC> GetNPC(int id);
        Task<ValidatorResult<NPC>> AddNPC(NPCDto npcDto);
        Task<ValidatorResult<NPC>> UpdateNPC(int id, NPCDto npcDto);
        Task<NPC> DeleteNPC(int id);
        Task<ValidatorResult<NPC>> Attack(int attackerId, int defenderId);
    }
}
