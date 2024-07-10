using RPGManager.WarstwaDomenowa.Models;
using RPGManager.WarstwaWprowadzania.Dtos;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RPGManager.WarstwaWprowadzania.Services.Interfaces
{
    public interface IPlayerCharacterService
    {
        Task<IEnumerable<PlayerCharacter>> GetPCs(CancellationToken cancellationToken);
        Task<PlayerCharacter> GetPC(int id);
        Task<PlayerCharacter> AddPC(PlayerCharacterDto pcDto);
        Task<IEnumerable<PlayerCharacter>> GetPCsByUserName(string userName, CancellationToken cancellationToken);
    }
}
