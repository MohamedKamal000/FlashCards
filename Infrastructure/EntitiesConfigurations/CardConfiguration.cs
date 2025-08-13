using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntitiesConfigurations;

public class CardConfiguration : IEntityTypeConfiguration<Card>
{
    public void Configure(EntityTypeBuilder<Card> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).
            ValueGeneratedNever().HasConversion(UlidValueConvertor.CreateConvertor()).HasColumnType("varchar(26)");
        
        
        builder.Property(c => c.CardTitle).IsRequired().HasMaxLength(100);
        builder.Property(c => c.Question).IsRequired().HasMaxLength(1000);
        builder.Property(c => c.Answer).IsRequired().HasMaxLength(1000);
        builder.Property(c => c.PicturePath).HasMaxLength(200);

        builder.HasOne(c => c.DeckUsedIn)
            .WithMany(d => d.Cards)
            .HasForeignKey(c => c.DeckId);
    }
}

