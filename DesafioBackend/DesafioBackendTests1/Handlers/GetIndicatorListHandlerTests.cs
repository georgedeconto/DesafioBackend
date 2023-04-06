using Microsoft.VisualStudio.TestTools.UnitTesting;
using DesafioBackend.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using DesafioBackend.DataBase;
using Microsoft.Data.Sqlite;
using DesafioBackend.Indicators;
using DesafioBackend.Queries;

namespace DesafioBackend.Handlers.Tests
{
    public class GetIndicatorListHandlerTests
    {
        private DesafioBackendContext _context;
        private GetIndicatorListHandler _handler;

        public GetIndicatorListHandlerTests()
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
            _handler = new GetIndicatorListHandler(_context);
        }

        [Fact]
        public async Task GetIndicatorListHandler_ShouldReturnIndicatorList()
        {
            //arrange
            var indicator1 = new Indicator(name: "indicator sum", resultType: EnumResult.Sum);
            indicator1.AddDataCollectionPoint(DateTime.Today.AddDays(-5), 100);
            indicator1.AddDataCollectionPoint(DateTime.Today.AddDays(-4), 50);

            var indicator2 = new Indicator(name: "indicator average", resultType: EnumResult.Average);
            indicator2.AddDataCollectionPoint(DateTime.Today.AddDays(-2), 11.6);
            indicator2.AddDataCollectionPoint(DateTime.Today.AddDays(-3), 5.9);

            await _context.AddAsync(indicator1);
            await _context.AddAsync(indicator2);

            await _context.SaveChangesAsync();

            var command = new GetIndicatorListQuery();

            //act
            var response = await _handler.Handle(command, default);

            //assert
            response.GetType().Should().Be(typeof(List<IndicatorViewModel>));
            response.Count.Should().Be(2);
        }

    }
}