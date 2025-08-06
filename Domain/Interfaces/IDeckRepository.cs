using System.Linq.Expressions;
using Domain.Entities;

namespace Domain.Interfaces;

public interface IDeckRepository : IRepository<Deck>
{
    public Task AddRangeOfCards(IEnumerable<Card> cards, Deck deck);

    public Task RemoveCards(IEnumerable<Card> card, Deck deck);
    
    public Task<IEnumerable<Deck>> TakePublicDecksPortion(Expression<Func<Deck,bool>> expression
        ,int pageSize, int index);
    
}