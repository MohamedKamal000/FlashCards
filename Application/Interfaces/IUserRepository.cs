using Domain.Entities;

namespace Domain.Interfaces;

public interface IUserRepository : IRepository<User>
{
    public Task AddDeckToCollection(User user,Deck deck);
    
    public Task RemoveDeckFromCollection(User user,Deck deck);

    public Task<IEnumerable<Deck>> GetPortionOfReferencedDecks(User user,int pageSize,int index);
}