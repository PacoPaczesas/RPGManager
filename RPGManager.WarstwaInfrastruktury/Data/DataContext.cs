using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RPGManager.WarstwaDomenowa.Models;
using RPGManager.WarstwaWprowadzania.Data;

namespace RPGManager.WarstwaInfrastruktury.Data
{
    public class DataContext : IdentityDbContext<Users>, IDataContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<NPC> NPCs { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Goods> Goods { get; set; }
        public DbSet<CountryGoods> CountryGoods { get; set; }
        public DbSet<PlayerCharacter> PlayerCharacters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<NPC>()
                .HasOne(n => n.Country)
                .WithMany()
                .HasForeignKey(n => n.CountryId);

            modelBuilder.Entity<NPC>()
                .HasMany(n => n.Notes)
                .WithOne(n => n.NPC)
                .HasForeignKey(n => n.NPCId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CountryGoods>()
                .HasKey(cg => new { cg.CountryId, cg.GoodsId });

            modelBuilder.Entity<CountryGoods>()
                .HasOne(cg => cg.Country)
                .WithMany(c => c.CountryGoods)
                .HasForeignKey(cg => cg.CountryId);

            modelBuilder.Entity<CountryGoods>()
                .HasOne(cg => cg.Goods)
                .WithMany(g => g.CountryGoods)
                .HasForeignKey(cg => cg.GoodsId);

            modelBuilder.Entity<PlayerCharacter>()
                .HasOne(pc => pc.User)
                .WithMany(u => u.PlayerCharacters)
                .HasForeignKey(pc => pc.UserName)
                .HasPrincipalKey(u => u.UserName);
        }
    }
}
