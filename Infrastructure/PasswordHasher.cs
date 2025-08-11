using System.Security.Cryptography;
using Domain.Interfaces;

namespace Infrastructure;

public class PasswordHasher : IPasswordHasher
{
    private const int HASH_SIZE = 32;
    private const int SALT_SIZE = 16;
    private const int ITERATIONS = 100000;
    
    
    public string Hash(string password)
    {
        byte[] salt = new byte[SALT_SIZE];
        using RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
        randomNumberGenerator.GetBytes(salt);


        using Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt, ITERATIONS);
        byte[] hash = pbkdf2.GetBytes(HASH_SIZE);

        using (var sha512 = SHA512.Create())
        {
            var result = sha512.ComputeHash(hash);

            return $"{Convert.ToBase64String(result)}-{Convert.ToBase64String(salt)}";
        }

    }

    public bool Verify(string inputPassword, string enteredPassword)
    {
        string[] splitedPassword = inputPassword.Split('-');

        byte[] hash = Convert.FromBase64String(splitedPassword[0]);
        byte[] salt = Convert.FromBase64String(splitedPassword[1]);
        
        using (var pbdkf2 = new Rfc2898DeriveBytes(enteredPassword, salt, ITERATIONS))
        {
            byte[] key = pbdkf2.GetBytes(HASH_SIZE);
            using (var sha512 = SHA512.Create())
            {
                byte[] enteredHash = sha512.ComputeHash(key);

                if (hash.Length != enteredHash.Length)
                    return false;
                
                return CryptographicOperations.FixedTimeEquals(hash,enteredHash); 
            }
        }

    }
}