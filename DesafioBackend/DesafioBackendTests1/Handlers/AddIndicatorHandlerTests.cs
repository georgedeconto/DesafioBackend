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

namespace DesafioBackend.Handlers.Tests
{
    [TestClass()]
    public class AddIndicatorHandlerTests
    {
        private DesafioBackendContext _context;
        private AddIndicatorHandler _handler;

        public AddIndicatorHandlerTests()
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
            _handler = new AddIndicatorHandler(_context);
        }

        [Fact]
        public async Task AddIndicatorHandler_ShouldAddIndicator()
        {
            //arrange
            var indicator1 = new Indicator(name: "indicator sum", resultType: EnumResult.Sum);
            indicator1.AddDataCollectionPoint(DateTime.Today.AddDays(-5), 100);
            indicator1.AddDataCollectionPoint(DateTime.Today.AddDays(-4), 50);

            var newIndicatorName = "new name";
            var newIndicatorResultType = EnumResult.Sum;

            await _context.AddAsync(indicator1);

            await _context.SaveChangesAsync();

            var command = new AddIndicatorCommand(newIndicatorName, newIndicatorResultType);


            //act
            await _handler.Handle(command, default);

            //assert
            var indicators = _context.IndicatorList;
            indicators.Should().HaveCount(2);
            indicators.FirstOrDefault(x=> x.Name == newIndicatorName)
                .ResultType.Should().Be(newIndicatorResultType);
        }
    }
}