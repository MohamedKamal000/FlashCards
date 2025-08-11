namespace Domain.Interfaces;

public interface IPasswordHasher
{
    public string Hash(string password);

    public bool Verify(string userPassword, string enteredPassword);
}