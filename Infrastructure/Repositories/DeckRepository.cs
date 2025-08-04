using System.Linq.Expressions;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class DeckRepository : GenericRepository<Deck>, IDeckRepository
{
    public DeckRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task AddRangeOfCards(IEnumerable<Card> cards, Deck deck)
    {
        await _dbContext.Entry(deck).Collection(d => d.Cards).LoadAsync();


        foreach (var card in cards)
        {
            deck.Cards.Add(card);
        }
    }

    // validation is done in Application Layer :>
    public async Task<IEnumerable<Deck>> TakePublicDecksPortion(Expression<Func<Deck, bool>> expression, int pageSize,
        int index)
    {
        var result = _dbContext.Decks.Where(expression).OrderBy(d => d.Id)
            .Skip(pageSize * index).Take(pageSize);

        return await result.ToListAsync();
    }
}