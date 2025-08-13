using Application.Dtos.UserDtos;
using Application.Services;
using Application.Utilities;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure;
using Infrastructure.EntitiesConfigurations;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using DeckRepository = Infrastructure.Repositories.DeckRepository;

namespace ManualTest;

class Program
{
    static void Main(string[] args)
    {
        DbContextOptionsBuilder<ApplicationDbContext> optionsBuilder =
            new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseSqlServer(
            "Server=.;Database=FlashCardsDb;Integrated Security=True;TrustServerCertificate=True;");

        var options = optionsBuilder.Options;

        ApplicationDbContext applicationDbContext = new ApplicationDbContext(options);
        IUserRepository userRepository = new UserRepository(applicationDbContext);
        IDeckRepository deckRepository = new DeckRepository(applicationDbContext);
        IRepository<Card> cardRepository = new GenericRepository<Card>(applicationDbContext);
        IRepository<CardStatus> cardStatusRepository = new GenericRepository<CardStatus>(applicationDbContext);
        IRepository<Note> note = new GenericRepository<Note>(applicationDbContext);
        IUnitOfWork unitOfWork = new UnitOfWork(applicationDbContext, userRepository, deckRepository, cardRepository,
            cardStatusRepository, note);
        IPasswordHasher passwordHasher = new PasswordHasher();
        
        AuthService authService = new AuthService(unitOfWork,passwordHasher);
        


    }
}