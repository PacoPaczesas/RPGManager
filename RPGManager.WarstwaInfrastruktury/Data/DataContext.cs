using Microsoft.EntityFrameworkCore;
using RPGManager.WarstwaDomenowa.Models;
using RPGManager.WarstwaWprowadzania.Data;

namespace RPGManager.WarstwaInfrastruktury.Data;

public class DataContext : DbContext, IDataContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    public DbSet<NPC> NPCs { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Note> Notes { get; set; }
    public DbSet<Goods> Goods { get; set; }
    public DbSet<CountryGoods> CountryGoods { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<NPC>()
            .HasOne(n => n.Country)
            .WithMany()
            .HasForeignKey(n => n.CountryId);

        modelBuilder.Entity<NPC>()
            .HasMany(n => n.Notes) // NPC ma wiele Notatek
            .WithOne(n => n.NPC) // Każda Notatka jest przypisana do jednego NPC
            .HasForeignKey(n => n.NPCId) // Klucz obcy w Notes wskazujący na NPC
            .OnDelete(DeleteBehavior.Cascade); // Określa zachowanie przy kasowaniu NPC, kasuje wszytstkie przypisane do NPC notatki


        // Konfiguracja relacji wiele do wielu między Country i Goods
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
    }

/*    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return base.SaveChangesAsync(cancellationToken);
    }*/


}
