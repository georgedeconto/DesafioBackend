using Microsoft.VisualStudio.TestTools.UnitTesting;
using DesafioBackend.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesafioBackend.DataBase;
using DesafioBackend.Indicators;
using DesafioBackend.Queries;
using DesafioBackend.Handlers;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;
using FluentAssertions;

namespace DesafioBackend.Handlers.Tests
{
    [TestClass()]
    public class GetIndicatorResultHandlerTests
    {
        private DesafioBackendContext _context;
        private GetIndicatorResultHandler _handler;

        public GetIndicatorResultHandlerTests()
        {
            var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<DesafioBackendContext>()
                .UseSqlite(connection)
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging()
                .Options;

            _context = new DesafioBackendContext(options);
            _context.Database.EnsureCreated();
            _handler = new GetIndicatorResultHandler(_context);
        }

        [Fact]
        public async Task GetIndicatorResultHandler_ShouldReturnIndicatorResult()
        {
            //arrange
            var indicator1 = new Indicator(name: "indicator sum", resultType: EnumResult.Sum);
            indicator1.AddDataCollectionPoint(DateTime.Today.AddDays(-5), 100);
            indicator1.AddDataCollectionPoint(DateTime.Today.AddDays(-4), 50);

            await _context.AddAsync(indicator1);

            await _context.SaveChangesAsync();

            var command = new GetIndicatorResultQuery(indicator1.Id);

            //act
            var response = await _handler.Handle(command, default);

            //assert
            response.Should().Be(indicator1.CalculateResult());
        }
    }
}