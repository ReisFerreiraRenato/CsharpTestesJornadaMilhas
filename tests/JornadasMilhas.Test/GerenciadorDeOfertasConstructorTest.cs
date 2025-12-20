using JornadaMilhas.Gerenciador;
using JornadaMilhas.Modelos;
using JornadaMilhas.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;

namespace JornadasMilhas.Test
{
    public class GerenciadorDeOfertasConstructorTest
    {
        [Fact]
        public void RetornarTrueComOfertaValida()
        {
            // Arrange
            var lista = new List<OfertaViagem>();
            var gerenciador = new GerenciadorDeOfertas(lista);

            var oferta = new OfertaViagem(
                new Rota("São Paulo", "Rio"),
                new Periodo(DateTime.Today, DateTime.Today.AddDays(5)),
                500);

            // Act
            var resultado = gerenciador.AdicionarOfertaNaLista(oferta);

            // Assert
            Assert.True(resultado);
            Assert.True(lista.Count > 0);
        }

        [Fact]
        public void RetornaFalseComOfertaNula()
        {
            // Arrange
            var lista = new List<OfertaViagem>();
            var gerenciador = new GerenciadorDeOfertas(lista);

            // Act
            var resultado = gerenciador.AdicionarOfertaNaLista(null!);

            // Assert
            Assert.False(resultado);
            Assert.Empty(lista);
        }

        [Fact]
        public void RetornaTresOfertasAoCriarAListaOfertaViagem()
        {
            // Arrange
            var lista = new List<OfertaViagem>();
            var gerenciador = new GerenciadorDeOfertas(lista);

            // Act
            gerenciador.CarregarOfertas();

            // Assert
            Assert.Equal(3, lista.Count);
        }

        [Fact]
        public void RetronarUmaOfertaCadastradaManualmente()
        {
            // Arrange
            var lista = new List<OfertaViagem>();
            var gerenciador = new GerenciadorDeOfertas(lista);

            var input =
                @"São Paulo
                Rio de Janeiro
                10/01/2024
                20/01/2024
                500";

            Console.SetIn(new StringReader(input));
            Console.SetOut(new StringWriter());

            // Act
            gerenciador.CadastrarOferta();

            // Assert
            Assert.Single(lista);
        }

        [Fact]
        public void RetornaListaVaziaQuandoCadastrarOfertaComDataInvalida()
        {
            // arrange
            var lista = new List<OfertaViagem>();
            var gerenciador = new GerenciadorDeOfertas(lista);

            var input =
                @"São Paulo
                Rio
                10/01/2024
                data_errada
                500";

            Console.SetIn(new StringReader(input));
            Console.SetOut(new StringWriter());

            // act
            gerenciador.CadastrarOferta();

            // assert
            Assert.Empty(lista);
        }

        [Fact]
        public void RetornaListaVaziaQuandoCadastrarOfertaComPrecoInvalido()
        {
            var lista = new List<OfertaViagem>();
            var gerenciador = new GerenciadorDeOfertas(lista);

            var input =
                @"São Paulo
                Rio
                10/01/2024
                20/01/2024
                abc";

            Console.SetIn(new StringReader(input));
            Console.SetOut(new StringWriter());

            gerenciador.CadastrarOferta();

            Assert.Empty(lista);
        }

        [Fact]
        public void RetornarTodasAsOfertasCadastradasNoMetodoExibirTodasAsOfertas()
        {
            var lista = new List<OfertaViagem>();
            var gerenciador = new GerenciadorDeOfertas(lista);

            gerenciador.CarregarOfertas();

            var output = new StringWriter();
            Console.SetOut(output);

            // Assert
            output.ToString().Should().Contain("Todas as ofertas cadastradas");
            //Assert.Contains("Todas as ofertas cadastradas", output.ToString().ToString());
        }
    }
}
