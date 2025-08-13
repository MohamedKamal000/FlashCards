using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.EntitiesConfigurations;

public static class UlidValueConvertor 
{
    public static ValueConverter<Ulid, string> CreateConvertor()
    {
        return new ValueConverter<Ulid, string>(
            v => v.ToString(),           // Convert Ulid → string for the database
            v => Ulid.Parse(v)           // Convert string → Ulid for the app
        );
    }
}