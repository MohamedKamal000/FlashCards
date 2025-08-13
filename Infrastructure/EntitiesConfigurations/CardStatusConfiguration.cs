using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntitiesConfigurations;

public class CardStatusConfiguration : IEntityTypeConfiguration<CardStatus>
{
    public void Configure(EntityTypeBuilder<CardStatus> builder)
    {
        builder.HasKey(cs => cs.Id);
        builder.Property(cs => cs.Id)
            .ValueGeneratedNever().HasConversion(UlidValueConvertor.CreateConvertor()).HasColumnType("varchar(26)");

        builder.Property(cs => cs.NeedsRevision).IsRequired();

        builder.HasOne(cs => cs.User)
            .WithMany(u => u.RevisionList)
            .HasForeignKey(cs => cs.UserId);

        builder.HasOne(cs => cs.Card).WithMany()
            .HasForeignKey(cs => cs.CardId).IsRequired(false);
    }
}

