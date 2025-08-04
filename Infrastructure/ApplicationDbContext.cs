using Domain.Entities;
using Infrastructure.EntitiesConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new CardConfiguration());
        modelBuilder.ApplyConfiguration(new CardStatusConfiguration());
        modelBuilder.ApplyConfiguration(new DeckConfiguration());
        modelBuilder.ApplyConfiguration(new NoteConfiguration());
    }
    
    public DbSet<User> Users { get; set; } 
    
    public DbSet<Card> Cards { get; set; }
    
    public DbSet<Deck> Decks { get; set; }
    
    public DbSet<Note> Notes { get; set; }
    
    public DbSet<CardStatus> CardsStatus { get; set; }
}