using Microsoft.VisualStudio.TestTools.UnitTesting;
using DesafioBackend.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesafioBackend.Commands;
using DesafioBackend.DataBase;
using DesafioBackend.Indicators;
using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DesafioBackend.Handlers.Tests
{
    public class DeleteDataCollectionPointHandlerTests
    {
        private DesafioBackendContext _context;
        private DeleteDataCollectionPointHandler _handler;

        public DeleteDataCollectionPointHandlerTests()
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
            _handler = new DeleteDataCollectionPointHandler(_context);
        }

        [Fact]
        public async Task DeleteDataCollectionPointHandler_ShouldDeleteDataCollectionPoint()
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

            var selectedDCP = indicator1.DataCollectionPoints[0];
            var remainingDCP = indicator1.DataCollectionPoints[1];

            var command = new DeleteDataCollectionPointCommand(indicator1.Id, selectedDCP.Date);

            //act
            await _handler.Handle(command, default);

            //assert
            _context.IndicatorList.FirstOrDefault().DataCollectionPoints[0].Should().Be(remainingDCP);
            _context.IndicatorList.Should().HaveCount(2);
        }

        [Fact]
        public async Task DeleteIndicatorDataCollectionPointHandler_ShouldThrowException_WhenIdDoesntExist()
        {
            //arrange
            //arrange
            var indicator1 = new Indicator(name: "indicator sum", resultType: EnumResult.Sum);
            indicator1.AddDataCollectionPoint(DateTime.Today.AddDays(-5), 100);
            indicator1.AddDataCollectionPoint(DateTime.Today.AddDays(-4), 50);

            await _context.AddAsync(indicator1);
            await _context.SaveChangesAsync();

            var selectedDCP = indicator1.DataCollectionPoints[0];
            var command = new DeleteDataCollectionPointCommand(Guid.NewGuid(), selectedDCP.Date);

            //act
            var act = async () => await _handler.Handle(command, default);

            //assert
            await act.Should().ThrowAsync<InvalidOperationException>("*Indicator not found*");
        }

        [Fact]
        public async Task DeleteIndicatorDataCollectionPointHandler_ShouldThrowException_WhenDateDoesntExist()
        {
            //arrange
            //arrange
            var indicator1 = new Indicator(name: "indicator sum", resultType: EnumResult.Sum);
            indicator1.AddDataCollectionPoint(DateTime.Today.AddDays(-5), 100);
            indicator1.AddDataCollectionPoint(DateTime.Today.AddDays(-4), 50);

            await _context.AddAsync(indicator1);
            await _context.SaveChangesAsync();

            var selectedDCP = indicator1.DataCollectionPoints[0];
            var command = new DeleteDataCollectionPointCommand(indicator1.Id, selectedDCP.Date.AddDays(-10));

            //act
            var act = async () => await _handler.Handle(command, default);

            //assert
            await act.Should().ThrowAsync<InvalidOperationException>("*Data Collection Point not found*");
        }
    }
}