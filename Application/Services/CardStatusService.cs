using Application.Dtos.CardDtos;
using Application.Respondbodies;
using Application.Utilities;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class CardStatusService
{
    private IUnitOfWork _unitOfWork { get; set; }
    
    public CardStatusService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }



    public async Task<Response<List<CardStatusDto>>> GetAllCardsStatus(string userId)
    {
        var user = await _unitOfWork.UserRepository.Get(userId);

        if (user is null)
            return Response<List<CardStatusDto>>.GetNotAcceptedRequest("User Id is not correct");

        var cards = (await _unitOfWork.CardStatusRepository.GetAll(c => c.User == user))
            .Select(c => new CardStatusDto(){CardTitle = c.Card.CardTitle, 
                Id = c.Id.ToString(), CardId = c.CardId.ToString()}).ToList();
        
        return Response<List<CardStatusDto>>.GetAcceptedRequest("Returned Successfully", cards);
    }
}