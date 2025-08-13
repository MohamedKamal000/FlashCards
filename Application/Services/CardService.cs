using Application.Dtos.CardDtos;
using Application.Respondbodies;
using Application.Utilities;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class CardService
{
    private readonly IUnitOfWork _unitOfWork;
    
    public CardService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }



    public async Task<Response<ViewCardDto>> ViewCertainCard(string cardId)
    {
        var card = await _unitOfWork.CardRepository.Get(cardId);

        if (card is null)
            return Response<ViewCardDto>.GetNotAcceptedRequest("Card id is not correct");


        var cardDto = EntityMapper.ProjectEntityToDto<ViewCardDto, Card>(card);

        return Response<ViewCardDto>.GetAcceptedRequest("Returned Successfully", cardDto);
    }

    public async Task<Response<string>> AddRangeOfCards(List<AddCardToDeckDto> cardsDto,string deckId)
    {
        var deck = await _unitOfWork.DeckRepository.Get(deckId);

        if (deck is null)
            return Response<string>.GetNotAcceptedRequest("deck id is not correct");

        var cards = cardsDto.Select(EntityMapper.ProjectDtoToEntity<AddCardToDeckDto, Card>).ToList();

        await _unitOfWork.CardRepository.AddRange(cards);
        await _unitOfWork.DeckRepository.AddRangeOfCards(cards,deck);

        await _unitOfWork.SaveChanges();
        
        return Response<string>.GetAcceptedRequest("Cards Added Successfully",deckId);
    }

    public async Task<Response<string>> RemoveCard(string cardId)
    {
        var card = await _unitOfWork.CardRepository.Get(cardId);

        if (card is null)
            return Response<string>.GetNotAcceptedRequest("Card id is not correct");
        
        
        _unitOfWork.CardRepository.Delete(card);
        await _unitOfWork.SaveChanges();

        return Response<string>.GetAcceptedRequest("Card is Removed ", null);
    }


    public async Task<Response<string>> MarkCard(string cardId,string userId, bool state)
    {
        var card = await _unitOfWork.CardRepository.Get(cardId);
        var user = await _unitOfWork.UserRepository.Get(userId);
        if (card is null)
            return Response<string>.GetNotAcceptedRequest("Card id is not correct");

        if (user is null)
            return Response<string>.GetNotAcceptedRequest("user id is not correct");

        
        var cardStatus = new CardStatus
        {
            NeedsRevision = state,
            Card = card,
            User = user
        };

        await _unitOfWork.CardStatusRepository.Add(cardStatus);
        await _unitOfWork.SaveChanges();

        return Response<string>.GetAcceptedRequest("Card status is created", cardStatus.Id.ToString());
    }

    
    
}


