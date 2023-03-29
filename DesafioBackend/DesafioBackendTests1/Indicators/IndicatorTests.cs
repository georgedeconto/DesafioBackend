using Xunit;
using FluentAssertions;
using DesafioBackend.Coletas;
using System.Collections.Generic;

namespace DesafioBackend.Indicators.Tests
{
    public class IndicatorTests
    {
        [Fact]
        public void Constructor_ShouldCreateIndicator()
        {
            //arrange
            var coletas = new List<Coleta>();
            var coleta1 = new Coleta(DateTime.Today, 100.0);
            var coleta2 = new Coleta(DateTime.Today, 200);
            coletas.Add(coleta1);
            coletas.Add(coleta2);
            var nome = "nome";
            var resultado = EnumResultado.Media;

            //act
            var indicator = new Indicator(nome, coletas, resultado);

            //assert
            indicator.Should().NotBeNull();
            indicator.Coletas.Should().Equal(coletas);
            indicator.Nome.Should().Be(nome);
            indicator.Resultado.Should().Be(resultado);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Constructor_ShouldThrowException_WhenNameIsEmptyOrWhiteSpace(string nome)
        {
            //arrange
            var coletas = new List<Coleta>();

            //act
            var act = () => new Indicator(nome, coletas, EnumResultado.Media);

            //assert
            act.Should().Throw<ArgumentException>("*O nome deve ser preenchido*");
        }

        [Fact]
        public void SetNome_ShouldSetNome()
        {
            //arrange
            var coletas = new List<Coleta>();
            var indicador = new Indicator("set nome", coletas, EnumResultado.Media);

            //act
            var novonome = "novo nome";
            indicador.SetNome(novonome);

            //assert
            indicador.Nome.Should().Be(novonome);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void SetNome_ShouldThrowException_WhenNomeIsEmptyOrWhiteSpace(string nome)
        {
            //arrange
            var coletas = new List<Coleta>();
            var indicador = new Indicator("set nome vazio", coletas, EnumResultado.Media);

            //act
            var act = () => indicador.SetNome(nome);

            //assert
            act.Should().Throw<ArgumentException>("*O nome deve ser preenchido*");
        }

        [Fact]
        public void AddColeta_ShouldAddColeta()
        {
            //arrange
            var coletas = new List<Coleta>();
            Coleta coleta1 = new(DateTime.Today, 100);
            Coleta coleta2 = new(DateTime.Today, 200);
            coletas.Add(coleta1);
            coletas.Add(coleta2);
            Indicator indicador = new("add coleta", coletas, EnumResultado.Media);

            //act
            Coleta coleta3 = new(DateTime.Today, 40);
            indicador.AddColeta(coleta3);

            //assert
            indicador.Coletas.Count().Should().Be(3);
            var lastColeta = indicador.Coletas.Last();
            lastColeta.Should().Be(coleta3);
        }

        [Fact]
        public void DeleteColeta_ShouldDeleteColeta()
        {
            //arrange
            var coletas = new List<Coleta>();
            Coleta coleta1 = new(DateTime.Today, 100);
            Coleta coleta2 = new(DateTime.Today, 200);
            coletas.Add(coleta1);
            coletas.Add(coleta2);
            Indicator indicador = new("delete coleta", coletas, EnumResultado.Media);

            //act
            indicador.DeleteColeta(coleta2.Id);

            //assert
            indicador.Coletas.Count().Should().Be(1);
            var coletaRestante = indicador.Coletas.First();
            coletaRestante.Should().Be(coleta1);
        }

        [Fact]
        public void DeleteColeta_ShouldThrowException_WhenColetaDoesNotExist()
        {
            //arrange
            var coletas = new List<Coleta>();
            Coleta coleta1 = new(DateTime.Today, 100.1);
            coletas.Add(coleta1);
            Indicator indicador = new("coleta nao existe", coletas, EnumResultado.Media);

            //act
            var act = () => indicador.DeleteColeta(Guid.NewGuid());

            //assert
            act.Should().Throw<ArgumentException>("*Coleta não encontrada*");
        }

        [Fact]
        public void EditColeta_ShouldEditColeta()
        {
            //arrange
            var coletas = new List<Coleta>();
            Coleta coleta1 = new(DateTime.Today, 100.1);
            Coleta coleta2 = new(DateTime.Today, 200);
            coletas.Add(coleta1);
            coletas.Add(coleta2);
            Indicator indicador = new("edit coleta", coletas, EnumResultado.Media);

            //act
            indicador.EditColeta(coleta1.Id, 40.5);

            //assert
            indicador.Coletas.First().Valor.Should().Be(40.5);
            indicador.Coletas.Last().Should().Be(coleta2);
        }

        [Fact]
        public void EditColeta_ShouldThrowException_WhenIdNotFound()
        {
            //arrange
            var coletas = new List<Coleta>();
            Coleta coleta1 = new(DateTime.Today, 100.1);
            Coleta coleta2 = new(DateTime.Today, 200);
            coletas.Add(coleta1);
            coletas.Add(coleta2);
            Indicator indicador = new("id coleta não encontrado", coletas, EnumResultado.Media);

            //act
            var act = () => indicador.EditColeta(Guid.NewGuid(), 40.5);

            //assert
            act.Should().Throw<ArgumentException>("*Coleta não encontrada*");
        }

        [Fact]
        public void CalculateResultado_ShouldCalculateResultadoSoma()
        {
            //arrange
            var coletas = new List<Coleta>();
            Coleta coleta1 = new(DateTime.Today, 1);
            Coleta coleta2 = new(DateTime.Today, 1);
            coletas.Add(coleta1);
            coletas.Add(coleta2);
            Indicator indicador = new("reusltado soma", coletas, EnumResultado.Soma);

            //act & assert
            indicador.CalculateResultado().Should().Be(2);
        }

        [Fact]
        public void CalculateResultado_ShouldCalculateResultadoMedia()
        {
            //arrange
            var coletas = new List<Coleta>();
            Coleta coleta1 = new(DateTime.Today, 1);
            Coleta coleta2 = new(DateTime.Today, 1);
            coletas.Add(coleta1);
            coletas.Add(coleta2);
            Indicator indicador = new("resultado media", coletas, EnumResultado.Media);

            //act & assert
            indicador.CalculateResultado().Should().Be(1);
        }

        [Fact]
        public void CalculateResultado_ShouldThrowException_WhenColetasIsEmpty()
        {
            //arrange
            var coletas = new List<Coleta>();
            Indicator indicador = new("coletas vazio", coletas, EnumResultado.Soma);

            //act
            var act = () => indicador.CalculateResultado();

            //assert
            act.Should().Throw<InvalidOperationException>("*Não há coletas neste indicador*");
        }
    }
}