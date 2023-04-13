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
using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;
using DesafioBackend.Commands;

namespace DesafioBackend.Handlers.Tests
{
    public class DeleteIndicatorHandlerTests
    {
        private DesafioBackendContext _context;
        private DeleteIndicatorHandler _handler;

        public DeleteIndicatorHandlerTests()
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
            _handler = new DeleteIndicatorHandler(_context);
        }

        [Fact]
        public async Task DeleteIndicatorHandler_ShouldDeleteIndicator()
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

            var command = new DeleteIndicatorCommand(indicator1.Id);


            //act
            await _handler.Handle(command, default);

            //assert
            _context.IndicatorList.Should().HaveCount(1);
            _context.IndicatorList.FirstOrDefault().Should().Be(indicator2);
        }

        [Fact]
        public async Task DeleteIndicatorHandler_ShouldThrowException_WhenIndicatorDoesntExist()
        {
            //arrange
            var command = new DeleteIndicatorCommand(Guid.NewGuid());

            //act
            var act = async () => await _handler.Handle(command, default);

            //assert
            act.Should().ThrowAsync<InvalidOperationException>("*404 NotFound*");
        }
    }
}