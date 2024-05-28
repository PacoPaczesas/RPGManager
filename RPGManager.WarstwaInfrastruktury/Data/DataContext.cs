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

    }
}
