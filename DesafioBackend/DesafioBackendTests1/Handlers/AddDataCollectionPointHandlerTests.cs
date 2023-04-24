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
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;
using FluentAssertions;
using System.Web.Http;

namespace DesafioBackend.Handlers.Tests
{
    public class AddDataCollectionPointHandlerTests
    {
        private DesafioBackendContext _context;
        private AddDataCollectionPointHandler _handler;

        public AddDataCollectionPointHandlerTests()
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
            _handler = new AddDataCollectionPointHandler(_context);
        }

        [Fact]
        public async Task AddDataCollectionPointHandler_ShouldAddDataCollectionPoint()
        {
            //arrange
            var indicator1 = new Indicator(name: "indicator sum", resultType: EnumResult.Sum);

            await _context.AddAsync(indicator1);
            await _context.SaveChangesAsync();

            var newDCPDate = DateTime.Today.AddDays(-1);
            var newDCPValue = 10;

            var command = new AddDataCollectionPointCommand(indicator1.Id, newDCPDate, newDCPValue);


            //act
            await _handler.Handle(command, default);

            //assert
            indicator1.DataCollectionPoints.Should().HaveCount(1);
            var newDCP = indicator1.DataCollectionPoints.First();
            newDCP.Date.Should().Be(newDCPDate);
            newDCP.Value.Should().Be(newDCPValue);
        }

        [Fact]
        public async Task AddDataCollectionPointHandler_ShouldThrowException_WhenIndicatorDoesntExist()
        {
            //arrange
            var newDCPDate = DateTime.Today.AddDays(-1);
            var newDCPValue = 10;

            var command = new AddDataCollectionPointCommand(Guid.NewGuid(), newDCPDate, newDCPValue);

            //act
            var act = async () => await _handler.Handle(command, default);

            //assert
            await act.Should().ThrowAsync<HttpResponseException>("*404*");
        }
    }
}