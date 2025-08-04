using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.EntitiesConfigurations;

public static class UlidValueConvertor 
{
    public static ValueConverter<Ulid, byte []> CreateConvertor()
    {
        return new ValueConverter<Ulid, byte []>(
            v => v.ToByteArray(),           // Convert Ulid → string for the database
            v => Ulid.Parse(v)           // Convert string → Ulid for the app
        );
    }
}