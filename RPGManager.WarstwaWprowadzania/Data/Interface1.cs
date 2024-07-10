using Microsoft.EntityFrameworkCore;
using RPGManager.WarstwaDomenowa.Models;
using System.Threading;
using System.Threading.Tasks;

namespace RPGManager.WarstwaWprowadzania.Data
{
    public interface IDataContext
    {
        DbSet<NPC> NPCs { get; set; }
        DbSet<Country> Countries { get; set; }
        DbSet<Note> Notes { get; set; }
        DbSet<Goods> Goods { get; set; }
        DbSet<CountryGoods> CountryGoods { get; set; }
        DbSet<Users> Users { get; set; }
        DbSet<PlayerCharacter> PlayerCharacters { get; set; }
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
