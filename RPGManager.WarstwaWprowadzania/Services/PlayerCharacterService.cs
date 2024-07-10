using RPGManager.WarstwaDomenowa.Models;
using RPGManager.WarstwaWprowadzania.Data;
using RPGManager.WarstwaWprowadzania.Dtos;
using RPGManager.WarstwaWprowadzania.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RPGManager.WarstwaWprowadzania.Services
{
    public class PlayerCharacterService : IPlayerCharacterService
    {
        private readonly IDataContext _context;

        public PlayerCharacterService(IDataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PlayerCharacter>> GetPCs(CancellationToken cancellationToken)
        {
            return await _context.PlayerCharacters.ToListAsync(cancellationToken);
        }

        public async Task<PlayerCharacter> GetPC(int id)
        {
            return await _context.PlayerCharacters.FindAsync(id);
        }

        public async Task<PlayerCharacter> AddPC(PlayerCharacterDto pcDto)
        {
            var playerCharacter = new PlayerCharacter(pcDto.Exp, pcDto.Strength, pcDto.Might)
            {
                Name = pcDto.Name,
                UserName = pcDto.UserName // Use UserName
            };

            _context.PlayerCharacters.Add(playerCharacter);
            await _context.SaveChangesAsync();

            return playerCharacter;
        }

        public async Task<IEnumerable<PlayerCharacter>> GetPCsByUserName(string userName, CancellationToken cancellationToken)
        {
            return await _context.PlayerCharacters
                .Include(pc => pc.User)
                .Where(pc => pc.UserName == userName)
                .ToListAsync(cancellationToken);
        }
    }
}
