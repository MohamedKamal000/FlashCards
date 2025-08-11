using Application.Dtos.CardDtos;
using Application.Dtos.DeckDto;
using Application.Respondbodies;
using Application.Utilities;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class DeckSetsService
{
    private readonly IUnitOfWork _unitOfWork;


    public DeckSetsService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<string>> CreateDeck(CreateDeckDto createDeckDto)
    {
        // validation...

        var userCreator = await _unitOfWork.UserRepository.Get(createDeckDto.UserId);
        if (userCreator is null)
            return Response<string>.GetNotAcceptedRequest("User Not Found");


        Deck deck = EntityMapper.ProjectDtoToEntity<CreateDeckDto, Deck>(createDeckDto);
        deck.CreatorUser = userCreator;

        await _unitOfWork.DeckRepository.Add(deck);
        await _unitOfWork.SaveChanges();

        return Response<string>.GetAcceptedRequest("Deck Created!", "");
    }

    public async Task<Response<List<ViewDeckDto>>> GetUserCreatedSets(string userId,
        int index, int pageSize)
    {
        // validation...
        var userCreator = await _unitOfWork.UserRepository.Get(userId);
        if (userCreator is null)
            return Response<List<ViewDeckDto>>.GetNotAcceptedRequest("User Not Found");

        List<ViewDeckDto> sets = (await _unitOfWork.DeckRepository
                .TakePortionOfSetsWithFilter(d => d.CreatorId == userCreator.Id
                                                  && !d.IsDeletedByCreator, pageSize, index))
            .Select(EntityMapper.ProjectEntityToDto<ViewDeckDto, Deck>).ToList();

        return Response<List<ViewDeckDto>>.GetAcceptedRequest("Result is Returned", sets);
    }


    public async Task<Response<List<ViewDeckDto>>> GetUserReferencedSets(string userId,
        int index, int pageSize)
    {
        // validation...
        var user = await _unitOfWork.UserRepository.Get(userId);
        if (user is null)
            return Response<List<ViewDeckDto>>.GetNotAcceptedRequest("User Not Found");

        List<ViewDeckDto> sets = (await _unitOfWork.UserRepository.GetPortionOfReferencedDecks(user, pageSize, index))
            .Select(EntityMapper.ProjectEntityToDto<ViewDeckDto, Deck>).ToList();

        return Response<List<ViewDeckDto>>.GetAcceptedRequest("Result is Returned", sets);
    }


    public async Task<Response<ViewDeckDto>> ViewCertainDeck(string deckId)
    {
        var deck = await _unitOfWork.DeckRepository.Get(deckId);

        if (deck is null)
            return Response<ViewDeckDto>.GetNotAcceptedRequest("Deck Id is not correct");

        var deckDto = EntityMapper.ProjectEntityToDto<ViewDeckDto, Deck>(deck);

        return Response<ViewDeckDto>.GetAcceptedRequest("Set is returned ", deckDto);
    }

    public async Task<Response<List<ViewCardDto>>> GetSetCards(string deckId)
    {
        var deck = await _unitOfWork.DeckRepository.Get(deckId);

        if (deck is null)
            return Response<List<ViewCardDto>>.GetNotAcceptedRequest("Deck Id is not correct");

        var cards = await _unitOfWork.CardRepository
            .GetAll(c => c.DeckId == deck.Id);

        var cardsDto = cards.Select(EntityMapper.ProjectEntityToDto<ViewCardDto, Card>).ToList();

        return Response<List<ViewCardDto>>.GetAcceptedRequest("Returned Successfully", cardsDto);
    }


    public async Task<Response<List<ViewDeckDto>>> NavigateSetsWithCategory(int index, int pageSize,
        Category deckCategory)
    {
        // validation on index,PageSize ...

        var decks = await _unitOfWork.DeckRepository.TakePortionOfSetsWithFilter(
            d => d.DeckCategory == deckCategory && d.IsPublic,
            pageSize, index);

        var decksDto = decks.Select(EntityMapper.ProjectEntityToDto<ViewDeckDto, Deck>).ToList();

        return Response<List<ViewDeckDto>>.GetAcceptedRequest("Returned Successfully", decksDto);
    }


    public async Task<Response<string>> AddSetToCollection(string deckId, string userId)
    {
        var user = await _unitOfWork.UserRepository.Get(userId);
        var deck = await _unitOfWork.DeckRepository.Get(deckId);

        if (user is null)
            return Response<string>.GetNotAcceptedRequest("User Id is not correct");
        if (deck is null)
            return Response<string>.GetNotAcceptedRequest("deck Id is not correct");


        await _unitOfWork.UserRepository.AddDeckToCollection(user, deck);

        await _unitOfWork.SaveChanges();

        return Response<string>.GetAcceptedRequest("Deck Added To Collection Successfully", null);
    }

    public async Task<Response<string>> RemoveDeck(string deckId, string userId, bool removeFromReferenced)
    {
        var user = await _unitOfWork.UserRepository.Get(userId);
        var deck = await _unitOfWork.DeckRepository.Get(deckId);

        if (user is null)
            return Response<string>.GetNotAcceptedRequest("User Id is not correct");
        if (deck is null)
            return Response<string>.GetNotAcceptedRequest("deck Id is not correct");

        if (removeFromReferenced)
        {
            await _unitOfWork.UserRepository.RemoveDeckFromCollection(user, deck);
            if (deck.IsDeletedByCreator)
            {
                _unitOfWork.DeckRepository.Delete(deck);
            }
        }

        else
            _unitOfWork.DeckRepository.Delete(deck);

        await _unitOfWork.SaveChanges();


        return Response<string>.GetAcceptedRequest("Deck Removed Successfully", null);
    }

    public async Task<Response<ViewDeckDto>> ChangeDeckDetails(ChangeDeckDto deckDto)
    {
        var deck = await _unitOfWork.DeckRepository.Get(deckDto.DeckId);

        if (deck is null)
            return Response<ViewDeckDto>.GetNotAcceptedRequest("deck id is not correct");

        deck.DeckName = deckDto.DeckName;
        deck.IsPublic = deckDto.IsPublic;
        deck.DeckDescription = deckDto.DeckDescription;

        var deckViewResult = EntityMapper.ProjectEntityToDto<ViewDeckDto, Deck>(deck);

        await _unitOfWork.SaveChanges();
        return Response<ViewDeckDto>.GetAcceptedRequest("Deck has been changed", deckViewResult);
    }
}