using System;
using System.Collections.Generic;
using System.Text;
using JornadaMilhas.Modelos;
using JornadaMilhas.Utils;

namespace JornadasMilhas.Test
{
    public class RotaConstructorTest
    {
        #pragma warning disable CS8600 // Conversão de literal nula ou possível valor nulo em tipo não anulável.
        #pragma warning disable CS8604 // Possível argumento de referência nula.
        [Fact]
        public void RetornaVerdadeiroQuandoDadosPreenchidosCorretos()
        {
            // arrange
            var origem = Constantes.ORIGEM_TESTE;
            var destino = Constantes.DESTINO_TESTE;

            // act
            Rota rota = new(origem, destino);

            // Assert
            Assert.Equal(destino, rota.Destino);
            Assert.Equal(origem, rota.Origem);
            Assert.True(rota.EhValido);
        }

        [Fact]
        public void RetornaErroQuandoRotaOrigemVazia()
        {
            // arrange
            var origem = string.Empty;
            var destino = Constantes.DESTINO_TESTE;

            // act
            Rota rota = new(origem, destino);

            // Assert
            Assert.True(condition: rota.Erros.Any());
            Assert.False(rota.EhValido);
        }

        [Fact]
        public void RetornaErroQuandoRotaDestinoVazia()
        {
            // arrange
            var origem = Constantes.ORIGEM_TESTE;
            var destino = string.Empty;

            // act
            Rota rota = new(origem, destino);

            // Assert
            Assert.True(condition: rota.Erros.Any());
            Assert.False(rota.EhValido);
        }

        [Fact]
        public void RetornaErroQuandoRotaOrigemNulo()
        {
            // arrange
            string origem = null;
            var destino = Constantes.DESTINO_TESTE;

            // act
            Rota rota = new(origem, destino);

            // Assert
            Assert.True(condition: rota.Erros.Any());
            Assert.False(rota.EhValido);
        }

        [Fact]
        public void RetornaErroQuandoRotaDestinoNula()
        {
            // arrange
            var origem = Constantes.ORIGEM_TESTE;
            string destino = null;

            // act
            Rota rota = new(origem, destino);

            // Assert
            Assert.True(condition: rota.Erros.Any());
            Assert.False(rota.EhValido);
        }

        #pragma warning restore CS8600 // Conversão de literal nula ou possível valor nulo em tipo não anulável.
        #pragma warning restore CS8604 // Possível argumento de referência nula.
    }
}
