using Application.Dtos.DeckDto;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlashCards.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeckSetsController : ControllerBase
    {
        private readonly DeckSetsService _deckSetsService;
        public DeckSetsController(DeckSetsService deckSetsService)
        {
            _deckSetsService = deckSetsService;
        }

        [HttpPost("CreateDeck")]
        public async Task<IActionResult> CreateDeck([FromBody] CreateDeckDto dto)
        {
            var result = await _deckSetsService.CreateDeck(dto);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("GetUserDecks/{userId}")]
        public async Task<IActionResult> GetUserDecks(string userId, int index = 0, int pageSize = 10)
        {
            var result = await _deckSetsService.GetUserCreatedSets(userId, index, pageSize);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("GetUserReferencedSets/{userId}")]
        public async Task<IActionResult> GetUserReferencedSets(string userId, int index = 0, int pageSize = 10)
        {
            var result = await _deckSetsService.GetUserReferencedSets(userId, index, pageSize);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("ViewDeck/{deckId}")]
        public async Task<IActionResult> ViewCertainDeck(string deckId)
        {
            var result = await _deckSetsService.ViewCertainDeck(deckId);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("GetSetCards/{deckId}")]
        public async Task<IActionResult> GetSetCards(string deckId)
        {
            var result = await _deckSetsService.GetSetCards(deckId);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("NavigateSetsWithCategory")]
        public async Task<IActionResult> NavigateSetsWithCategory(int index, int pageSize, [FromQuery] int category)
        {
            var result = await _deckSetsService.NavigateSetsWithCategory(index, pageSize, (Domain.Entities.Category)category);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("AddSetToCollection")]
        public async Task<IActionResult> AddSetToCollection([FromQuery] string deckId, [FromQuery] string userId)
        {
            var result = await _deckSetsService.AddSetToCollection(deckId, userId);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpDelete("RemoveDeck")]
        public async Task<IActionResult> RemoveDeck([FromQuery] string deckId, [FromQuery] string userId, [FromQuery] bool removeFromReferenced)
        {
            var result = await _deckSetsService.RemoveDeck(deckId, userId, removeFromReferenced);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
