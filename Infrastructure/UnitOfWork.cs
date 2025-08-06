using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class UnitOfWork : IUnitOfWork
{

    private readonly ApplicationDbContext _dbContext;
    public IUserRepository UserRepository { get; set; }
    public IDeckRepository DeckRepository { get; set; }
    public IRepository<Card> CardRepository { get; set; }
    public IRepository<CardStatus> CardStatusRepository { get; set; }
    public IRepository<Note> NoteRepository { get; set; }

    
    public UnitOfWork(
        ApplicationDbContext dbContext,
        IUserRepository userRepository,
        IDeckRepository deckRepository,
        IRepository<Card> cardRepository,
        IRepository<CardStatus> cardStatusRepository,
        IRepository<Note> noteRepository)
    {
        _dbContext = dbContext;
        UserRepository = userRepository;
        DeckRepository = deckRepository;
        CardRepository = cardRepository;
        CardStatusRepository = cardStatusRepository;
        NoteRepository = noteRepository;
    }


    public async Task<int> SaveChanges()
    {
        return await _dbContext.SaveChangesAsync();
    }
}
