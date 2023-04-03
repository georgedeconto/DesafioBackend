using Xunit;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;

namespace DesafioBackend.DataCollection.Tests
{
    public class DataCollectionPointTests
    {
        [Fact]
        public void Constructor_ShouldCreateDataCollectionPoint()
        {
            //arrange
            var indicatorId = Guid.NewGuid();
            var date = DateTime.Now;
            var value = 10.34;

            //act
            var dataCollectionPoint = new DataCollectionPoint(indicatorId, date, value);

            //assert
            dataCollectionPoint.Should().NotBeNull();
            dataCollectionPoint.IndicatorId.Should().Be(indicatorId);
            dataCollectionPoint.Date.Should().Be(date);
            dataCollectionPoint.Value.Should().Be(value);
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenFutureDate()
        {
            //arrange
            var indicatorId = Guid.NewGuid();
            var date = DateTime.Now.AddDays(1);
            var value = 10.34;

            //act
            var act = () => new DataCollectionPoint(indicatorId, date, value);

            //assert
            act.Should().Throw<ArgumentException>("*Data não pode ser no futuro*");
        }

        [Fact]
        public void SetValue_ShouldSetValue()
        {
            //arrange
            var indicatorId = Guid.NewGuid();
            var date = DateTime.Now;
            var value = 10.34;
            var dataCollectionPoint = new DataCollectionPoint(indicatorId, date, value);
            var newValue = 11;

            //act
            dataCollectionPoint.SetValue(newValue);

            //assert
            dataCollectionPoint.Value.Should().Be(newValue);
        }
    }
}