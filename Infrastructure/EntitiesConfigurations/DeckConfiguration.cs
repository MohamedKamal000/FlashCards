using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntitiesConfigurations;

public class DeckConfiguration : IEntityTypeConfiguration<Deck>
{
    public void Configure(EntityTypeBuilder<Deck> builder)
    {
        builder.HasKey(d => d.Id);
        builder.Property(d => d.Id).
            ValueGeneratedNever().HasConversion(UlidValueConvertor.CreateConvertor())
            .HasColumnType("binary(16)");
     
        builder.Property(d => d.DeckName).IsRequired().HasMaxLength(100);
        builder.Property(d => d.DeckDescription).HasMaxLength(500);
        builder.Property(d => d.IsPublic).IsRequired();
        builder.Property(d => d.IsDeletedByCreator).HasDefaultValue(false);

        builder.HasOne(d => d.CreatorUser)
            .WithMany(u => u.CreatedDecks)
            .HasForeignKey(d => d.CreatorId).OnDelete(DeleteBehavior.NoAction);
        
        builder.Property(b => b.DeckCategory).HasConversion<string>();
        
        builder.HasMany(d => d.Cards)
            .WithOne(c => c.DeckUsedIn)
            .HasForeignKey(c => c.DeckId).OnDelete(DeleteBehavior.Cascade);
    }
}

