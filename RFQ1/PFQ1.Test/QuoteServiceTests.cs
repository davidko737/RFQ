using Xunit;
using PFQ1;
using RFQ1.Entities;
using RFQ1.Repositories.Interface;
using RFQ1.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Moq;

namespace PFQ1.Test
{
    public class QuoteServiceTests
    {
        private readonly Mock<IEquityInstrumentRepository> _mockEquityRepository;
        private readonly Mock<IQuoteRepository> _mockQuoteRepository;
        private readonly QuoteService _quoteService;

        public QuoteServiceTests()
        {
            _mockEquityRepository = new Mock<IEquityInstrumentRepository>();
            _mockQuoteRepository = new Mock<IQuoteRepository>();
            _quoteService = new QuoteService(_mockEquityRepository.Object, _mockQuoteRepository.Object);
        }

        [Fact]
        public async Task RequestQuoteAsync_ValidTicker_ReturnsQuote()
        {
            // Arrange
            var ticker = "AAPL";
            var quantity = 10;
            var equity = new EquityInstrument { Ticker = ticker };
            _mockEquityRepository.Setup(repo => repo.GetEquityInstrumentAsync(ticker)).ReturnsAsync(equity);

            // Act
            var result = await _quoteService.RequestQuoteAsync(ticker, quantity);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(ticker, result.Ticker);
            Assert.Equal(quantity, result.Quantity);
            _mockQuoteRepository.Verify(repo => repo.SaveQuoteAsync(It.IsAny<Quote>()), Times.Once);
        }

        [Fact]
        public async Task RequestQuoteAsync_InvalidTicker_ThrowsArgumentException()
        {
            // Arrange
            var ticker = ""; // Invalid ticker
            var quantity = 10;

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _quoteService.RequestQuoteAsync(ticker, quantity));
            Assert.Equal("Ticker cannot be null or empty. (Parameter 'ticker')", exception.Message);
        }

        [Fact]
        public async Task AcceptQuoteAsync_ValidQuoteId_ReturnsTrue()
        {
            // Arrange
            var quoteId = 1;
            var quote = new Quote { Id = quoteId };
            _mockQuoteRepository.Setup(repo => repo.GetQuoteByIdAsync(quoteId)).ReturnsAsync(quote);
            _mockQuoteRepository.Setup(repo => repo.UpdateQuoteAsync(quote)).ReturnsAsync(true);

            // Act
            var result = await _quoteService.AcceptQuoteAsync(quoteId);

            // Assert
            Assert.True(result);
            Assert.True(quote.IsAccepted);
            Assert.NotNull(quote.AcceptedTime);
        }

        [Fact]
        public async Task AcceptQuoteAsync_InvalidQuoteId_ThrowsArgumentException()
        {
            // Arrange
            var quoteId = 999; // Non-existent quote ID
            _mockQuoteRepository.Setup(repo => repo.GetQuoteByIdAsync(quoteId)).ReturnsAsync((Quote)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _quoteService.AcceptQuoteAsync(quoteId));
            Assert.Equal("Quote not found. (Parameter 'quoteId')", exception.Message);
        }

        [Fact]
        public async Task GetAllQuotesAsync_ReturnsListOfQuotes()
        {
            // Arrange
            var quotes = new List<Quote>
            {
                new Quote { Id = 1, Ticker = "AAPL" },
                new Quote { Id = 2, Ticker = "GOOGL" }
            };
            _mockQuoteRepository.Setup(repo => repo.GetAllQuotesAsync()).ReturnsAsync(quotes);

            // Act
            var result = await _quoteService.GetAllQuotesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }
    }
}