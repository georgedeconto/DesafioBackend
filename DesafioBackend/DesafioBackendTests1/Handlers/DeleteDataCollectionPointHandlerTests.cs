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
using System.Web.Http;

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
            var selectedDCPDate = DateTime.Today.AddDays(-5);
            var remainingDCPDate = DateTime.Today.AddDays(-4);
            indicator1.AddDataCollectionPoint(selectedDCPDate, 100);
            indicator1.AddDataCollectionPoint(remainingDCPDate, 50);

            await _context.Indicators.AddAsync(indicator1);

            await _context.SaveChangesAsync();

            var selectedDCP = indicator1.GetDataCollectionPoint(selectedDCPDate);
            var remainingDCP = indicator1.GetDataCollectionPoint(remainingDCPDate);

            var command = new DeleteDataCollectionPointCommand(indicator1.Id, selectedDCP.Date);

            //act
            await _handler.Handle(command, default);

            //assert
            _context.Indicators.FirstOrDefault().DataCollectionPoints.First().Should().Be(remainingDCP);
            _context.Indicators.FirstOrDefault().DataCollectionPoints.Should().HaveCount(1);
        }

        [Fact]
        public async Task DeleteIndicatorDataCollectionPointHandler_ShouldThrowException_WhenIndicatorDoesntExist()
        {
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
            await act.Should().ThrowAsync<HttpResponseException>("*404*");
        }

        [Fact]
        public async Task DeleteIndicatorDataCollectionPointHandler_ShouldThrowException_WhenThereIsNoDataCollectionPointForTheDate()
        {
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
            await act.Should().ThrowAsync<HttpResponseException>("*404*");
        }
    }
}