using Domain.Entities;

namespace Domain.Interfaces;

public interface IUnitOfWork
{
    public IUserRepository UserRepository { get; set; }
    public IDeckRepository DeckRepository { get; set; }
    public IRepository<Card> CardRepository { get; set; }
    public IRepository<CardStatus> CardStatusRepository { get; set; }
    public IRepository<Note> NoteRepository { get; set; }
    
    
    public Task<int> SaveChanges();
}