using Application.Dtos.CardDtos;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlashCards.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardController : ControllerBase
    {
        private readonly CardService _cardService;
        public CardController(CardService cardService)
        {
            _cardService = cardService;
        }

        [HttpGet("GetCard/{cardId}")]
        public async Task<IActionResult> GetCard(string cardId)
        {
            var result = await _cardService.ViewCertainCard(cardId);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("AddRange/{deckId}")]
        public async Task<IActionResult> AddRange([FromBody] List<AddCardToDeckDto> cardsDto, string deckId)
        {
            var result = await _cardService.AddRangeOfCards(cardsDto, deckId);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("MarkCard")]
        public async Task<IActionResult> MarkCard([FromQuery] string cardId, [FromQuery] string userId, [FromQuery] bool state)
        {
            var result = await _cardService.MarkCard(cardId, userId, state);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpDelete("RemoveCard/{cardId}")]
        public async Task<IActionResult> RemoveCard(string cardId)
        {
            var result = await _cardService.RemoveCard(cardId);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
