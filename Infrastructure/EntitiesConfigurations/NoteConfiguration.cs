using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntitiesConfigurations;

public class NoteConfiguration : IEntityTypeConfiguration<Note>
{
    public void Configure(EntityTypeBuilder<Note> builder)
    {
        builder.HasKey(n => n.Id);
        builder.Property(n => n.Id).
            ValueGeneratedNever()
            .HasConversion(UlidValueConvertor.CreateConvertor()).
            HasColumnType("varchar(26)");
        
        builder.Property(n => n.NoteFilePath).IsRequired().HasMaxLength(200);

        builder.HasOne(n => n.User)
            .WithMany(u => u.Notes)
            .HasForeignKey(n => n.UserId);
    }
}

