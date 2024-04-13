using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using RPGManager.Dtos;
using RPGManager.Models;

namespace RPGManager.Services.Interfaces
{
    public interface INPCService
    {
        IEnumerable<NPC> GetNPCs();
        NPC GetNPC(int id);
        (NPC, Validator) AddNPC(NPCDto npcDto);
        NPC UpdateNPC(int id, NPCDto npcDto);
        NPC DeleteNPC(int id);
    }
}
