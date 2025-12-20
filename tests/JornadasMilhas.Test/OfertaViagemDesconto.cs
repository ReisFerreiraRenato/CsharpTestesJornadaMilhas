using JornadaMilhas.Modelos;
using JornadaMilhas.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace JornadasMilhas.Test
{
    public class OfertaViagemDesconto
    {
        [Theory]
        [InlineData(100, 20)]
        public void RetornaPrecoAtualizadoQuandoAplicadoDesconto
            (double precoOriginal, double desconto)
        {
            // arrange
            Rota rota = new(Constantes.ORIGEM_TESTE, Constantes.DESTINO_TESTE);
            Periodo periodo = new(DateTime.Parse("2024-02-01"), DateTime.Parse("2024-02-05"));
            double precoComDesconto = precoOriginal - desconto;

            OfertaViagem oferta = new(rota, periodo, precoOriginal)
            {
                // act 
                Desconto = desconto
            };

            // assert
            Assert.Equal(precoComDesconto, oferta.Preco);
        }

        [Theory]
        [InlineData(100, 120, 30)]
        [InlineData(100, 100, 30)]
        public void RetornaDescontoMaximoQuandoValorDescontoMaiorOuIgualAoPreco
            (double precoOriginal, double desconto, double precoComDesconto)
        {
            // arrange
            Rota rota = new(Constantes.ORIGEM_TESTE, Constantes.DESTINO_TESTE);
            Periodo periodo = new(DateTime.Parse("2024-02-01"), DateTime.Parse("2024-02-05"));
            OfertaViagem oferta = new(rota, periodo, precoOriginal)
            {
                // act 
                Desconto = desconto
            };

            // assert
            Assert.Equal(precoComDesconto, oferta.Preco, 0.001);
        }

        [Theory]
        [InlineData(- 150)]
        public void RetornaDescontoMaximoQuandoValorDescontoNegativo
            (double desconto)
        {
            // arrange
            Rota rota = new(Constantes.ORIGEM_TESTE, Constantes.DESTINO_TESTE);
            Periodo periodo = new(DateTime.Parse("2024-02-01"), DateTime.Parse("2024-02-05"));
            double precoComDesconto = 100; // Desconto negativo ou zero, se mantem o preço

            OfertaViagem oferta = new(rota, periodo, precoComDesconto)
            {
                // act 
                Desconto = desconto
            };

            // assert
            Assert.Equal(precoComDesconto, oferta.Preco, 0.001);
        }
        [Theory]
        [InlineData(100, 0)]
        public void RetornaDescontoMaximoQuandoValorDescontoIgualAZero
            (double precoOriginal, double desconto)
        {
            // arrange
            Rota rota = new(Constantes.ORIGEM_TESTE, Constantes.DESTINO_TESTE);
            Periodo periodo = new(DateTime.Parse("2024-02-01"), DateTime.Parse("2024-02-05"));
            double precoComDesconto = precoOriginal; // Desconto negativo ou zero, se mantem o preço

            OfertaViagem oferta = new(rota, periodo, precoOriginal)
            {
                // act 
                Desconto = desconto
            };

            // assert
            Assert.Equal(precoComDesconto, oferta.Preco, 0.001);
        }
    }
}
