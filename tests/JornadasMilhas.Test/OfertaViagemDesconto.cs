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
        [InlineData(Constantes.ORIGEM_TESTE, Constantes.DESTINO_TESTE, "2024-02-01", "2024-02-05", 100, 20)]
        public void RetornaPrecoAtualizadoQuandoAplicadoDesconto
            (string origem, string destino, string dataida, string datavolta, double precoOriginal, double desconto)
        {
            // arrange
            Rota rota = new(origem, destino);
            Periodo periodo = new(DateTime.Parse(dataida), DateTime.Parse(datavolta));
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
        [InlineData(Constantes.ORIGEM_TESTE, Constantes.DESTINO_TESTE, "2024-02-01", "2024-02-05", 100, 120)]
        public void RetornaDescontoMaximoQuandoValorDescontoMaiorQuePreco
            (string origem, string destino, string dataida, string datavolta, double precoOriginal, double desconto)
        {
            // arrange
            Rota rota = new(origem, destino);
            Periodo periodo = new(DateTime.Parse(dataida), DateTime.Parse(datavolta));
            double precoComDesconto = precoOriginal * 0.30; // Desconto de 70%

            OfertaViagem oferta = new(rota, periodo, precoOriginal)
            {
                // act 
                Desconto = desconto
            };

            // assert
            Assert.Equal(precoComDesconto, oferta.Preco, 0.001);
        }

        [Theory]
        [InlineData(Constantes.ORIGEM_TESTE, Constantes.DESTINO_TESTE, "2024-02-01", "2024-02-05", 100, - 150)]
        public void RetornaDescontoMaximoQuandoValorDescontoNegativo
            (string origem, string destino, string dataida, string datavolta, double precoOriginal, double desconto)
        {
            // arrange
            Rota rota = new(origem, destino);
            Periodo periodo = new(DateTime.Parse(dataida), DateTime.Parse(datavolta));
            double precoComDesconto = precoOriginal; // Desconto negativo, se mantem o preço

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
