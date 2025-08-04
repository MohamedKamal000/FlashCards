using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task AddDeckToCollection(User user, Deck deck)
    {
        await _dbContext.Entry(user).Collection(u => u.ReferencedDecks).LoadAsync();
        user.ReferencedDecks.Add(deck);
    }

    public async Task RemoveDeckFromCollection(User user, Deck deck)
    {
        await _dbContext.Entry(user).Collection(u => u.ReferencedDecks).LoadAsync();
        user.ReferencedDecks.Remove(deck);

        // will do the deck cleanup in Application Layer
        // if the deck removed from the collection does not have any other referenced user
        // and the user who created it marked it as removed
        // it gets removed from the deck table 
    }

    // i need to do pagenation or whatever it called here...
    // i will end up loading all entities in order to do the pagentation thing anyway
    public async Task<IEnumerable<Deck>> GetPortionOfDecks(User user, int pageSize, int index)
    {
        var userId = user.Id; // Make sure this is accessible
        return await _dbContext.Users
            .Where(u => u.Id == userId)
            .SelectMany(u => u.ReferencedDecks)
            .OrderBy(d => d.Id) 
            .Skip(pageSize * index)
            .Take(pageSize).ToListAsync();
    }

    public override void Delete(User obj)
    {
        _dbContext.Entry(obj).Collection(u => u.CreatedDecks).Load();

        var userDecks = obj.CreatedDecks.ToList();

        userDecks.ForEach(d => d.IsDeletedByCreator = true);
    }
}