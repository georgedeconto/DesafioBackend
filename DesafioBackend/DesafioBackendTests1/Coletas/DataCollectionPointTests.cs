using Xunit;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;

namespace DesafioBackend.Coletas.Tests
{
    public class DataCollectionPointTests
    {
        [Fact]
        public void Should_CreateDataCollectionPoint()
        {
            //arrange
            var indicatorId = Guid.NewGuid();
            var date = DateTime.Now;
            var value = 10.34;

            //act
            var datacollectionpoint = new DataCollectionPoint(indicatorId, date, value);

            //assert
            datacollectionpoint.Should().NotBeNull();
            datacollectionpoint.IndicatorId.Should().Be(indicatorId);
            datacollectionpoint.Date.Should().Be(date);
            datacollectionpoint.Value.Should().Be(value);
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenFutureDate()
        {
            //arrange
            var indicatorId = Guid.NewGuid();
            var date = DateTime.Now.AddDays(1);
            var value = 10.34;

            //act and assert
            var act = () => new DataCollectionPoint(indicatorId, date, value);
            act.Should().Throw<ArgumentException>("*Data não pode ser no futuro*");
        }

        [Fact]
        public void SetValue_ShouldSetValue()
        {
            //arrange
            var indicatorId = Guid.NewGuid();
            var date = DateTime.Now;
            var value = 10.34;
            var datacollectionpoint = new DataCollectionPoint(indicatorId, date, value);

            //act
            var newvalue = 11;
            datacollectionpoint.SetValue(newvalue);

            //assert
            datacollectionpoint.Value.Should().Be(newvalue);
        }
    }
}