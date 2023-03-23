using Xunit;
using FluentAssertions;
using DesafioBackend.Coletas;

namespace DesafioBackend.Indicators.Tests
{
    public class IndicatorTests
    {
        [Fact]
        public void Should_CreateIndicator()
        {
            //arrange
            List<Coleta> lista = new List<Coleta>();
            Coleta coleta1 = new(DateTime.Today, 100.0);
            Coleta coleta2 = new(DateTime.Today, 200);
            lista.Add(coleta1);
            lista.Add(coleta2);

            //act
            Indicator indic = new("nome", lista, EnumResultado.Media);

            //assert
            indic.Should().NotBeNull();
            indic.Coletas.Should().Equal(lista);
            indic.Nome.Should().Be("nome");
            indic.Resultado.Should().Be(EnumResultado.Media);
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenNameIsEmpty()
        {
            //arrange
            List<Coleta> lista = new List<Coleta>();

            //act
            var act = () => new Indicator(string.Empty, lista, EnumResultado.Media);

            //assert
            act.Should().Throw<ArgumentException>("*O nome deve ser preenchido*");
        }

        [Fact]
        public void Constructor_ShouldThrowException_WhenNameIsWhiteSpace()
        {
            //arrange
            List<Coleta> lista = new List<Coleta>();

            //act
            var act = () => new Indicator("   ", lista, EnumResultado.Media);

            //assert
            act.Should().Throw<ArgumentException>("*O nome deve ser preenchido*");
        }


        [Fact]
        public void Should_AddColeta()
        {
            //arrange
            List<Coleta> lista = new List<Coleta>();
            Coleta coleta1 = new(DateTime.Today, 100);
            Coleta coleta2 = new(DateTime.Today, 200);
            lista.Add(coleta1);
            lista.Add(coleta2);
            Indicator indic = new("nome", lista, EnumResultado.Media);

            //act
            Coleta coleta3 = new(DateTime.Today, 40);
            indic.AddColeta(coleta3);

            //assert
            indic.Coletas.Count().Should().Be(3);
            var lastColeta = indic.Coletas.Last();
            lastColeta.Should().Be(coleta3);
        }

        [Fact]
        public void Should_DeleteColeta()
        {
            //arrange
            List<Coleta> lista = new List<Coleta>();
            Coleta coleta1 = new(DateTime.Today, 100.1);
            Coleta coleta2 = new(DateTime.Today, 200);
            lista.Add(coleta1);
            lista.Add(coleta2);
            Indicator indic = new("nome", lista, EnumResultado.Media);

            //act
            indic.DeleteColeta(coleta2.Id);

            //assert
            indic.Coletas.Count().Should().Be(1);
            var coletaRestante = indic.Coletas.First();
            coletaRestante.Should().Be(coleta1);
        }

        [Fact]
        public void DeleteColeta_ShouldThrowException_WhenPersonDoesNotExist()
        {
            //arrange
            List<Coleta> lista = new List<Coleta>();
            Coleta coleta1 = new(DateTime.Today, 100.1);
            lista.Add(coleta1);
            Indicator indic = new("nome", lista, EnumResultado.Media);

            //act
            var act = () => indic.DeleteColeta(Guid.NewGuid());

            //assert
            act.Should().Throw<ArgumentException>("*Coleta não encontrada*");
        }

        [Fact]
        public void Should_EditColeta()
        {
            //arrange
            List<Coleta> lista = new List<Coleta>();
            Coleta coleta1 = new(DateTime.Today, 100.1);
            Coleta coleta2 = new(DateTime.Today, 200);
            lista.Add(coleta1);
            lista.Add(coleta2);
            Indicator indic = new("nome", lista, EnumResultado.Media);

            //act
            indic.EditColeta(coleta1.Id, 40.5);

            //assert
            indic.Coletas.First().Valor.Should().Be(40.5);
            indic.Coletas.Last().Should().Be(coleta2);
        }

        [Fact]
        public void EditColeta_ShouldThrowException_WhenIdNotFound()
        {
            //arrange
            List<Coleta> lista = new List<Coleta>();
            Coleta coleta1 = new(DateTime.Today, 100.1);
            Coleta coleta2 = new(DateTime.Today, 200);
            lista.Add(coleta1);
            lista.Add(coleta2);
            Indicator indic = new("nome", lista, EnumResultado.Media);

            //act
            var act = () => indic.EditColeta(Guid.NewGuid(), 40.5);

            //assert
            act.Should().Throw<ArgumentException>("*Coleta não encontrada*");
        }

        [Fact]
        public void Should_CalcularResultadoSoma()
        {
            //arrange
            List<Coleta> lista = new List<Coleta>();
            Coleta coleta1 = new(DateTime.Today, 1);
            Coleta coleta2 = new(DateTime.Today, 1);
            lista.Add(coleta1);
            lista.Add(coleta2);
            Indicator indic = new("nome", lista, EnumResultado.Soma);

            //act & assert
            indic.CalcularResultado().Should().Be(2);
        }

        [Fact]
        public void Should_CalcularResultadoMedia()
        {
            //arrange
            List<Coleta> lista = new List<Coleta>();
            Coleta coleta1 = new(DateTime.Today, 1);
            Coleta coleta2 = new(DateTime.Today, 1);
            lista.Add(coleta1);
            lista.Add(coleta2);
            Indicator indic = new("nome", lista, EnumResultado.Media);

            //act & assert
            indic.CalcularResultado().Should().Be(1);
        }

        [Fact]
        public void CalcularResultado_ShouldThrowException_WhenColetasIsEmpty()
        {
            //arrange
            List<Coleta> lista = new List<Coleta>();
            Coleta coleta1 = new(DateTime.Today, 1);
            lista.Add(coleta1);
            Indicator indic = new("nome", lista, EnumResultado.Soma);
            indic.DeleteColeta(coleta1.Id);

            //act
            var act = () => indic.CalcularResultado();

            //assert
            act.Should().Throw<InvalidOperationException>("*Não há coletas neste indicador*");
        }
    }
}