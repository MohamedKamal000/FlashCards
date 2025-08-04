using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntitiesConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).
            ValueGeneratedNever().
            HasConversion(UlidValueConvertor.CreateConvertor())
            .HasColumnType("binary(16)");

        // fixing the multiple cascade paths error
        builder.HasMany(u => u.ReferencedDecks)
            .WithMany(d => d.ReferencedUsers)
            .UsingEntity<Dictionary<string, object>>(
                "ReferencedDeck_ReferencedUser",
                j => j
                    .HasOne<Deck>()
                    .WithMany()
                    .HasForeignKey("DeckId")
                    .OnDelete(DeleteBehavior.NoAction), // avoid multiple cascade paths
                j => j
                    .HasOne<User>()
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade), // allow cascade from user only
                j =>
                {
                    j.HasKey("DeckId", "UserId");
                    j.ToTable("ReferencedDeck_ReferencedUser");
                }
            );
        
        builder.HasMany(u => u.CreatedDecks).WithOne(d => d.CreatorUser)
            .HasForeignKey(d => d.CreatorId).OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(u => u.Notes).WithOne(n => n.User)
            .HasForeignKey(n => n.UserId).OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.RevisionList).WithOne(r => r.User)
            .HasForeignKey(r => r.UserId).OnDelete(DeleteBehavior.Cascade);

        
        
        builder.Property(u => u.Name).IsRequired().HasMaxLength(50);
        builder.Property(u => u.Password).IsRequired();
        builder.Property(u => u.Email).IsRequired().HasMaxLength(100);
        builder.HasIndex(u => u.Email).IsUnique();
        
    }
}