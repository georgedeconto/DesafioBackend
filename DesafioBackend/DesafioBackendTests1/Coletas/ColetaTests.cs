using Xunit;
using FluentAssertions;

namespace DesafioBackend.Coletas.Tests
{
    public class ColetaTests
    {
        [Fact]
        public void Should_CreateColeta()
        {
            //arrange
            var data = DateTime.Now;
            var valor = 10.34;

            //act
            var coleta = new Coleta(data, valor);

            //assert
            coleta.Should().NotBeNull();
            coleta.Date.Should().Be(data);
            coleta.Valor.Should().Be(valor);
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenFutureDate()
        {
            //arrange
            var data = DateTime.Now.AddDays(1);
            var valor = 10.34;

            //act and assert
            var act = () => new Coleta(data, valor);
            act.Should().Throw<ArgumentException>("*Data não pode ser no futuro*");
        }
    }
}