using JornadaMilhas.Utils;
using JornadaMilhas.Modelos;

namespace JornadasMilhas.Test
{
    public class OfertaViagemConstrutorTest
    {
        [Theory]
        [InlineData("", null, "2024-01-01", "2024-01-02", 0, false)]
        [InlineData(Constantes.ORIGEM_TESTE, Constantes.DESTINO_TESTE, "2024-02-01", "2024-02-05", 100, true)]
        [InlineData(null, "São Paulo", "2024-01-01", "2024-01-01", - 1, false)]
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
    }
}
