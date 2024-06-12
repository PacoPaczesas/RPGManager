using RPGManager.WarstwaDomenowa.Models;
using RPGManager.WarstwaWprowadzania.Dtos;

namespace RPGManager.WarstwaWprowadzania.Services.Interfaces
{
    public interface INPCService
    {
        Task<IEnumerable<NPC>> GetNPCs(CancellationToken token);
        Task<NPC> GetNPC(int id);
        Task<Result<NPC>> AddNPC(NPCDto npcDto);
        Task<Result<NPC>> UpdateNPC(int id, NPCDto npcDto);
        Task<NPC> DeleteNPC(int id);
        Task<Result<NPC>> Attack(int attackerId, int defenderId);
    }
}
