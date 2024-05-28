using RPGManager.Dtos;
using RPGManager.Models;

namespace RPGManager.Services.Interfaces
{
    public interface INPCService
    {
        IEnumerable<NPC> GetNPCs();
        NPC GetNPC(int id);
        ValidatorResult<NPC> AddNPC(NPCDto npcDto);
        ValidatorResult<NPC> UpdateNPC(int id, NPCDto npcDto);
        NPC DeleteNPC(int id);
        ValidatorResult<NPC> Attack(int attackerId, int defenderId);
    }
}
