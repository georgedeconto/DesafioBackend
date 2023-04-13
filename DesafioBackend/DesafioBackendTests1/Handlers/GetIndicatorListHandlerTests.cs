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
            var indicator1Name = "indicator sum";
            var indicator1Type = EnumResult.Sum;
            var indicator1 = new Indicator(name: indicator1Name, resultType: indicator1Type);

            var DCP1ADate = DateTime.Today.AddDays(-5);
            var DCP1AValue = 100;
            indicator1.AddDataCollectionPoint(DCP1ADate, DCP1AValue);

            var DCP1BDate = DateTime.Today.AddDays(-4);
            var DCP1BValue = 50;
            indicator1.AddDataCollectionPoint(DCP1BDate, DCP1BValue);

            var indicator2Name = "indicator average";
            var indicator2Type = EnumResult.Average;
            var indicator2 = new Indicator(name: indicator2Name, resultType: indicator2Type);

            var DCP2ADate = DateTime.Today.AddDays(-2);
            var DCP2AValue = 11.6;
            indicator2.AddDataCollectionPoint(DCP2ADate, DCP2AValue);

            var DCP2BDate = DateTime.Today.AddDays(-3);
            var DCP2BValue = 5.9;
            indicator2.AddDataCollectionPoint(DCP2BDate, DCP2BValue);

            await _context.AddAsync(indicator1);
            await _context.AddAsync(indicator2);

            await _context.SaveChangesAsync();

            var command = new GetIndicatorListQuery();

            //act
            var response = await _handler.Handle(command, default);

            //assert
            response.GetType().Should().Be(typeof(List<IndicatorViewModel>));
            response.Should().HaveCount(2);

            var selectedIndicator1 = response.FirstOrDefault(i => i.Name == indicator1Name);
            selectedIndicator1.ResultType.Should().Be(indicator1Type);

            var selectedDCP1A = selectedIndicator1.DataCollectionPoints.FirstOrDefault(d => d.Date == DCP1ADate);
            selectedDCP1A.Value.Should().Be(DCP1AValue);

            var selectedDCP1B = selectedIndicator1.DataCollectionPoints.FirstOrDefault(d => d.Date == DCP1BDate);
            selectedDCP1B.Value.Should().Be(DCP1BValue);

            var selectedIndicator2 = response.FirstOrDefault(i => i.Name == indicator2Name);
            selectedIndicator2.ResultType.Should().Be(indicator2Type);

            var selectedDCP2A = selectedIndicator2.DataCollectionPoints.FirstOrDefault(d => d.Date == DCP2ADate);
            selectedDCP2A.Value.Should().Be(DCP2AValue);

            var selectedDCP2B = selectedIndicator2.DataCollectionPoints.FirstOrDefault(d => d.Date == DCP2BDate);
            selectedDCP2B.Value.Should().Be(DCP2BValue);
        }

    }
}