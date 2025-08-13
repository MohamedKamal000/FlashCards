using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlashCards.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardStatusController : ControllerBase
    {
        private readonly CardStatusService _cardStatusService;
        public CardStatusController(CardStatusService cardStatusService)
        {
            _cardStatusService = cardStatusService;
        }

        [HttpGet("GetAllCardStatuses/{userId}")]
        public async Task<IActionResult> GetAllCardStatuses(string userId)
        {
            var result = await _cardStatusService.GetAllCardsStatus(userId);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
