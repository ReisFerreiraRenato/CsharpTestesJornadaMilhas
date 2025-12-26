using System;
using System.Collections.Generic;
using System.Text;
using JornadaMilhas.Gerenciador;
using JornadaMilhas.Modelos;

namespace JornadasMilhas.Test
{
    public class GerenciadodeOfertasRecuperarMaiorDescontoTest
    {
        [Fact]
        public void RetornaOfertaNulaQuandoListaEstáVazia()
        {
            // arrange
            var lista = new List<OfertaViagem>();
            var gerenciador = new GerenciadorDeOfertas(lista);
            Func<OfertaViagem, bool> filtro = o => o.Rota.Destino.Equals("São Paulo");


            // act
            var oferta = gerenciador.RecuperaMaiorDesconto(filtro);

            // assert
            Assert.Null(oferta);
        }

        [Fact]
        // destino - São Paulo, desconto = 40, preço 80
        public void RetornaOfertaEspecificaQuandoDestinoSaoPauloDesconto40()
        {
            // arrange
            var lista = new List<OfertaViagem>();
            var gerenciador = new GerenciadorDeOfertas(lista);
            Func<OfertaViagem, bool> filtro = o => o.Rota.Destino.Equals("São Paulo");
            var precoEsperado = 40;

            // act
            var oferta = gerenciador.RecuperaMaiorDesconto(filtro);

            // assert
            Assert.NotNull(oferta);
            Assert.Equal(precoEsperado, oferta.Preco, 0.0001);
        }
    }
}
