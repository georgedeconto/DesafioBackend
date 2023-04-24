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
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;
using FluentAssertions;
using System.Web.Http;

namespace DesafioBackend.Handlers.Tests
{
    public class GetIndicatorByIdHandlerTests
    {
        private DesafioBackendContext _context;
        private GetIndicatorByIdHandler _handler;

        public GetIndicatorByIdHandlerTests()
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
            _handler = new GetIndicatorByIdHandler(_context);
        }

        [Fact]
        public async Task GetIndicatorByIdHandler_ShouldReturnIndicator()
        {
            //arrange
            var indicator1 = new Indicator(name: "indicator sum", resultType: EnumResult.Sum);
            var DCP1Date = DateTime.Today.AddDays(-5);
            var DCP1Value = 100;
            var DCP2Date = DateTime.Today.AddDays(-4);
            var DCP2Value = 50;
            indicator1.AddDataCollectionPoint(DCP1Date, DCP1Value);
            indicator1.AddDataCollectionPoint(DCP2Date, DCP2Value);

            var indicator2 = new Indicator(name: "indicator average", resultType: EnumResult.Average);
            indicator2.AddDataCollectionPoint(DateTime.Today.AddDays(-2), 11.6);
            indicator2.AddDataCollectionPoint(DateTime.Today.AddDays(-3), 5.9);

            await _context.AddAsync(indicator1);
            await _context.AddAsync(indicator2);

            await _context.SaveChangesAsync();

            var command = new GetIndicatorByIdQuery(indicator1.Id);

            //act
            var response = await _handler.Handle(command, default);

            //assert
            response.Id.Should().Be(indicator1.Id);
            response.Name.Should().Be(indicator1.Name);
            response.ResultType.Should().Be(indicator1.ResultType);

            var DCP1 = response.DataCollectionPoints.FirstOrDefault(d => d.Date == DCP1Date);
            DCP1.Value.Should().Be(DCP1Value);

            var DCP2 = response.DataCollectionPoints.FirstOrDefault(d => d.Date == DCP2Date);
            DCP2.Value.Should().Be(DCP2Value);
        }

        [Fact]
        public async Task GetIndicatorByIdHandler_ShouldThrowException_WhenIndicatorDoesntExist()
        {
            //arrange
            var command = new GetIndicatorByIdQuery(Guid.NewGuid());

            //act
            var act = async () => await _handler.Handle(command, default);

            //assert
            act.Should().ThrowAsync<HttpResponseException>("*404*");
        }
    }
}