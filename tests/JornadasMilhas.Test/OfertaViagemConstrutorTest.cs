using JornadaMilhas.Utils;
using JornadaMilhas.Modelos;
using Microsoft.IdentityModel.Tokens;

namespace JornadasMilhas.Test
{
    public class OfertaViagemConstrutorTest
    {
    // desativando alguns warnings para referência nula dentro da minha classe de teste, preciso deles para realização dos mesmos.
    #pragma warning disable CS8600 // Conversão de literal nula ou possível valor nulo em tipo não anulável.
    #pragma warning disable CS8604 // Possível argumento de referência nula em objeto.
    #pragma warning disable xUnit1012 // Null should only be used for nullable parameters

        [Theory]
        [InlineData("", Constantes.STRING_NULA, "2024-01-01", "2024-01-02", 0, false)]
        [InlineData(Constantes.ORIGEM_TESTE, Constantes.DESTINO_TESTE, "2024-02-01", "2024-02-05", 100, true)]
        [InlineData(Constantes.STRING_NULA, "São Paulo", "2024-01-01", "2024-01-01", - 1, false)]
        [InlineData("Vitoria", "São Paulo", "2024-01-01", "2024-01-01", 0, false)]
        [InlineData("Rio de Janeiro", "São Paulo", "2024-01-01", "2024-01-01", - 500, false)]
        public void RetornaEhValidoDeAcordoComDadosDeEntrada
          (string origem, string destino, string dataida, string datavolta, double preco, bool validacao)
        {
            // padrão de teste (3A) triplo a
            // arrange
            Rota rota = new(origem, destino);
            Periodo periodo = new(DateTime.Parse(dataida), DateTime.Parse(datavolta));

            // act 
            OfertaViagem oferta = new(rota, periodo, preco);

            // assert
            Assert.Equal(validacao, oferta.EhValido);
        }

        [Fact]
        public void RetornaMensagemDeErroDeRotaInvalidaQuandoRotaNula()
        {
            // arrange
            Rota? rota = null;
            Periodo periodo = new(new DateTime(2024, 2, 1), new DateTime(2024, 2, 5));
            double preco = 100.0;

            // act
            #nullable disable
            OfertaViagem oferta = new(rota, periodo, preco);
            #nullable enable

            // assert
            Assert.Contains(Constantes.ERRO_ROTA_INVALIDA, oferta.Erros.Sumario);
            Assert.False(oferta.EhValido);
        }

        [Fact]
        public void RetornaMensagemToStringValidaQuandoDadosCorretos()
        { 
            Rota rota = new(Constantes.ORIGEM_TESTE, Constantes.DESTINO_TESTE);
            Periodo periodo = new(new DateTime(2024, 2, 1), new DateTime(2024, 2, 5));
            double preco = 100.0;
            string mensagemEsperada = $"Origem: {rota.Origem}, Destino: {rota.Destino}, Data de Ida: {periodo.DataInicial:d}, Data de Volta: {periodo.DataFinal:d}, Preço: {preco:C}";

            //act
            OfertaViagem oferta = new(rota, periodo, preco);

            // Assert
            Assert.Equal(mensagemEsperada, oferta.ToString());
        }

        [Fact]
        public void RetornaMensagemDeErroDePeriodoNuloQuandoPeriodoNulo()
        {
            // arrange
            Rota rota = new(Constantes.ORIGEM_TESTE, Constantes.DESTINO_TESTE);
            Periodo? periodo = null;
            double preco = 100.0;

            // act
            #nullable disable
            OfertaViagem oferta = new(rota, periodo, preco);
            #nullable enable
            
            // assert
            Assert.Contains(Constantes.ERRO_PERIODO_NULO, oferta.Erros.Sumario);
            Assert.False(oferta.EhValido);
        }

        [Fact]
        public void RetornaMensagemDeERRO_DATAS_INVALIDASQuandoDataIdaMaiorDataVolta()
        {
            // padrão de teste (3A) triplo a
            // arrange
            Rota rota = new(Constantes.ORIGEM_TESTE, Constantes.DESTINO_TESTE);
            Periodo periodo = new(new DateTime(2024, 2, 5), new DateTime(2024, 2, 1));
            double preco = 100.0;

            // act 
            OfertaViagem oferta = new(rota, periodo, preco);

            // assert
            Assert.Contains(Constantes.ERRO_DATAS_INVALIDAS, oferta.Erros.Sumario);
            Assert.False(oferta.EhValido);
        }

        [Fact]
        public void RetornaMensagemDeErroDePrecoInvalidoQuandoPrecoIgualAZero()
        {
            // Arrange
            Rota rota = new(Constantes.ORIGEM_TESTE, Constantes.DESTINO_TESTE);
            Periodo periodo = new(new DateTime(2024, 2, 1), new DateTime(2024, 2, 5));
            double preco = 0.0;

            // act
            OfertaViagem oferta = new(rota, periodo, preco);

            // assert
            Assert.Contains(Constantes.ERRO_PRECO_INVALIDO, oferta.Erros.Sumario);
            Assert.False(oferta.EhValido);
        }

        [Theory]
        [InlineData(Constantes.ORIGEM_TESTE, Constantes.DESTINO_TESTE, "2024-01-01", "2024-01-02", 650, true)]
        [InlineData(Constantes.ORIGEM_TESTE, Constantes.DESTINO_TESTE, "2024-01-01", "2024-01-02", 0, false)]
        [InlineData(Constantes.ORIGEM_TESTE, Constantes.DESTINO_TESTE, "2024-01-01", "2024-01-02", - 100, false)]
        public void RetornaMensagemPrecoDeAcordoComDadosDeEntrada
            (string origem, string destino, string dataida, string datavolta, double preco, bool validacao)
        {
            var msgErro = "";

            // arrange
            Rota rota = new(origem, destino);
            Periodo periodo = new(DateTime.Parse(dataida), DateTime.Parse(datavolta));
            if (!validacao)
            {
                msgErro = Constantes.ERRO_PRECO_INVALIDO;
            }

            // act
            OfertaViagem oferta = new(rota, periodo, preco);

            // assert
            Assert.Contains(msgErro, oferta.Erros.Sumario);
            Assert.Equal(validacao, oferta.EhValido);
        }

        [Fact]
        public void RetornaTresErrosDeValidacaoQuandoRotaPeriodoEPrecoSaoInvalidos()
        {
            // arrange
            int quantidadeEsperada = 3;
            Rota rota = null;
            Periodo periodo = null;// new(new DateTime(2024, 6,1), new DateTime(2024,5,10));
            double preco = Constantes.DOUBLE_NEGATIVO;

            // act
            OfertaViagem oferta = new(rota, periodo, preco);

            // assert
            Assert.Equal(quantidadeEsperada, oferta.Erros.Count());
        }

        [Fact]
        public void RetornaErrosDeValidacaoQuandoRotaPeriodoEPrecoSaoInvalidos()
        {
            // arrange
            Rota rota = null;
            Periodo periodo = null;// new(new DateTime(2024, 6,1), new DateTime(2024,5,10));
            double preco = Constantes.DOUBLE_NEGATIVO;

            // act
            OfertaViagem oferta = new(rota, periodo, preco);

            // assert
            Assert.NotNull(oferta.Erros.GetEnumerator().ToString());
        }

        [Theory]
        // act
        [InlineData(1, Constantes.ORIGEM_TESTE, Constantes.DESTINO_TESTE, "2024-01-01", "2024-01-02", -1)] // 1 erro preço negativo
        [InlineData(2, Constantes.ORIGEM_TESTE, Constantes.DESTINO_TESTE, "2024-03-10", "2024-01-02", -1)] // 2 erros preço negativo, data origem maior
        [InlineData(3, Constantes.STRING_NULA, Constantes.STRING_NULA, Constantes.STRING_NULA, Constantes.STRING_NULA, -1)] // 3 erros preço negativo, rota nula, data origem maior
        public void RetornaQuantidadeErrosConformeValoresDeEntrada
            (int qtdErrosEsperados, string origem, string destino, string dataida, string datavolta, double preco)
        {
            // act
            Periodo periodo = null;
            Rota rota = null;
            if (!origem.IsNullOrEmpty() && !destino.IsNullOrEmpty())
            {
                rota = new(origem, destino);
            }
            if (!dataida.IsNullOrEmpty() && !destino.IsNullOrEmpty())
            {
                periodo = new(DateTime.Parse(dataida), DateTime.Parse(datavolta));
            }
            OfertaViagem oferta = new(rota, periodo, preco);

            // assert

            Assert.Equal(qtdErrosEsperados, oferta.Erros.Count());
        }

    #pragma warning restore CS8600 // Conversão de literal nula ou possível valor nulo em tipo não anulável.
    #pragma warning restore CS8604 // Possível argumento de referência nula.
    #pragma warning restore xUnit1012 // Null should only be used for nullable parameters
    }
}
