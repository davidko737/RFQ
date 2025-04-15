using Microsoft.AspNetCore.Mvc;
using RFQ1.DTO;
using RFQ1.Services.Interface;
using System;
using System.Threading.Tasks;

namespace RFQ1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuoteController : ControllerBase
    {
        private readonly IQuoteService _quoteService;

        public QuoteController(IQuoteService quoteService)
        {
            _quoteService = quoteService;
        }

        [HttpPost("request")]
        public async Task<IActionResult> RequestQuote([FromBody] QuoteRequestDto request)
        {
            if (request == null)
            {
                return BadRequest("Request body cannot be null.");
            }

            return await HandleServiceOperation(async () =>
            {
                var quote = await _quoteService.RequestQuoteAsync(request.Ticker, request.Quantity);
                return Ok(quote);
            });
        }

        [HttpPost("accept/{quoteId}")]
        public async Task<IActionResult> AcceptQuote(int quoteId)
        {
            return await HandleServiceOperation(async () =>
            {
                bool isAccepted = await _quoteService.AcceptQuoteAsync(quoteId);
                return isAccepted ? Ok("Quote accepted successfully.") : NotFound("Quote not found or already accepted.");
            });
        }

        [HttpPost("cancel/{quoteId}")]
        public async Task<IActionResult> CancelQuote(int quoteId)
        {
            return await HandleServiceOperation(async () =>
            {
                bool isCanceled = await _quoteService.CancelQuoteAsync(quoteId);
                return isCanceled ? Ok("Quote canceled successfully.") : NotFound("Quote not found.");
            });
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllQuotes()
        {
            var quotes = await _quoteService.GetAllQuotesAsync();
            return Ok(quotes); 
        }

        private async Task<IActionResult> HandleServiceOperation(Func<Task<IActionResult>> operation)
        {
            try
            {
                return await operation();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
