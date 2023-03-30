using Xunit;
using FluentAssertions;
using DesafioBackend.DataCollection;
using System.Collections.Generic;

namespace DesafioBackend.Indicators.Tests
{
    public class IndicatorTests
    {
        [Fact]
        public void Constructor_ShouldCreateIndicator()
        {
            //arrange
            var name = "nome";
            var resultType = EnumResult.Average;

            //act
            var indicator = new Indicator(name: name, resultType: resultType);

            //assert
            indicator.Should().NotBeNull();
            indicator.Name.Should().Be(name);
            indicator.ResultType.Should().Be(resultType);
        }


        [Fact]
        public void SetName_ShouldSetName()
        {
            //arrange
            var indicador = new Indicator(name: "name", resultType: EnumResult.Average);
            var newname = "new name";

            //act
            indicador.SetName(newname);

            //assert
            indicador.Name.Should().Be(newname);
        }


        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void SetName_ShouldThrowException_WhenNameIsNullOrEmptyOrWhiteSpace(string name)
        {
            //arrange
            var indicator = new Indicator(name: "name", resultType: EnumResult.Average);

            //act
            var act = () => indicator.SetName(name);

            //assert
            act.Should().Throw<ArgumentException>("*O nome deve ser preenchido*");
        }

        [Fact]
        public void AddDataCollectionPoint_ShouldAddDataCollectionPoint()
        {
            //arrange
            var indicator = new Indicator(name: "name", resultType: EnumResult.Average);
            var date = DateTime.Today;
            var value = 40;

            //act
            indicator.AddDataCollectionPoint(date: date, value: value);

            //assert
            indicator.DataCollectionPoints.Should().HaveCount(1);
            var lastPoint = indicator.DataCollectionPoints.Last();
            lastPoint.Date.Should().Be(date);
            lastPoint.Value.Should().Be(value);
            lastPoint.IndicatorId.Should().Be(indicator.Id);
        }

        [Fact]
        public void AddDataCollectionPoint_ShouldThrowException_WhenDateIsAlreadyInDataColleciontPoints()
        {
            //arrange
            var indicator = new Indicator(name: "name", resultType: EnumResult.Average);
            var date = DateTime.Today;
            var value = 40;
            indicator.AddDataCollectionPoint(date: date, value: value);

            //act
            var act = () => indicator.AddDataCollectionPoint(date: date, value: value);

            //assert
            act.Should().Throw<ArgumentException>("*Já existe uma coleta nesta data*");
        }

        [Fact]
        public void DeleteDataCollectionPoint_ShouldDeleteDataCollectionPoint()
        {
            //arrange
            var indicator = new Indicator(name: "name", resultType: EnumResult.Average);
            var date1 = DateTime.Today;
            var value1 = 40;
            var date2 = DateTime.Today.AddDays(-1);
            var value2 = 50;
            indicator.AddDataCollectionPoint(date: date1, value: value1);
            indicator.AddDataCollectionPoint(date: date2, value: value2);

            //act
            indicator.DeleteDataCollectionPoint(date1);

            //assert
            indicator.DataCollectionPoints.Should().HaveCount(1);
            indicator.DataCollectionPoints.Last().Date.Should().Be(date2);
            indicator.DataCollectionPoints.Last().Value.Should().Be(value2);
        }

        [Fact]
        public void DeleteDataCollectionPoint_ShouldThrowException_WhenDataCollectionPointDoesNotExist()
        {
            //arrange
            var indicator = new Indicator(name: "name", resultType: EnumResult.Sum);

            //act
            var act = () => indicator.DeleteDataCollectionPoint(DateTime.Today);

            //assert
            act.Should().Throw<ArgumentException>("*Coleta não encontrada*");
        }

        [Fact]
        public void EditDataCollectionPoint_ShouldEditDataCollectionPoint()
        {
            //arrange
            var indicator = new Indicator(name: "name", resultType: EnumResult.Average);
            var date1 = DateTime.Today;
            var value1 = 40;
            var date2 = DateTime.Today.AddDays(-1);
            var value2 = 50;
            indicator.AddDataCollectionPoint(date: date1, value: value1);
            indicator.AddDataCollectionPoint(date: date2, value: value2);
            var newValue = 45;

            //act
            indicator.EditDataCollectionPoint(date1, newValue);

            //assert
            indicator.DataCollectionPoints.First().Value.Should().Be(newValue);
            indicator.DataCollectionPoints.First().Date.Should().Be(date1);
            indicator.DataCollectionPoints.Last().Value.Should().Be(value2);
            indicator.DataCollectionPoints.Should().HaveCount(2);
        }

        [Fact]
        public void EditDataCollectionPoint_ShouldThrowException_WhenDataCollectionPointDoesNotExist()
        {
            //arrange
            var indicator = new Indicator(name: "name", resultType: EnumResult.Average);
            var date = DateTime.Today;
            var value = 40;
            indicator.AddDataCollectionPoint(date: date, value: value);
            var nonExistentDate = DateTime.Today.AddDays(-1);
            var newValue = 50;

            //act
            var act = () => indicator.EditDataCollectionPoint(nonExistentDate, newValue);

            //assert
            act.Should().Throw<ArgumentException>("*Coleta não encontrada*");
        }

        [Fact]
        public void CalculateResult_ShouldCalculateResultSum()
        {
            //arrange
            var indicator = new Indicator(name: "name", resultType: EnumResult.Sum);
            var date1 = DateTime.Today;
            var value1 = 40;
            var date2 = DateTime.Today.AddDays(-1);
            var value2 = 50;
            indicator.AddDataCollectionPoint(date: date1, value: value1);
            indicator.AddDataCollectionPoint(date: date2, value: value2);

            //act & assert
            indicator.CalculateResult().Should().Be(value1 + value2);
        }

        [Fact]
        public void CalculateResult_ShouldCalculateResultAverage()
        {
            //arrange
            var indicator = new Indicator(name: "name", resultType: EnumResult.Average);
            var date1 = DateTime.Today;
            var value1 = 40;
            var date2 = DateTime.Today.AddDays(-1);
            var value2 = 50;
            indicator.AddDataCollectionPoint(date: date1, value: value1);
            indicator.AddDataCollectionPoint(date: date2, value: value2);

            //act & assert
            indicator.CalculateResult().Should().Be((value1 + value2) / 2);
        }

        [Fact]
        public void CalculateResultado_ShouldThrowException_WhenColetasIsEmpty()
        {
            //arrange
            var indicator = new Indicator(name: "name", resultType: EnumResult.Sum);

            //act
            var act = () => indicator.CalculateResult();

            //assert
            act.Should().Throw<InvalidOperationException>("*Não há coletas neste indicador*");
        }
    }
}