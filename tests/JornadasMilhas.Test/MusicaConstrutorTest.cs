using JornadaMilhas.Modelos;
using JornadaMilhas.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace JornadasMilhas.Test
{
    public class MusicaConstrutorTest
    {
        // Arrange
        private const string TITULO = "Casamento real é amanhã";
        private const string ARTISTA = "José de Arruda";
        private const string MUSICA_TESTE = "Musica Teste";
        private const string OUTRA_MUSICA_TESTE = "Outra " + MUSICA_TESTE;
        private const string MAIS_UMA_MUSICA_TESTE = "Mais uma " + MUSICA_TESTE;
        private const string ID = "Id: ";
        private const string NOME = "Nome: ";
        private const string STRING_1 = "1";
        private const string STRING_2 = "2";
        private const string STRING_3 = "3";
        private const int INTEIRO_1 = 1;
        private const int INTEIRO_2 = 2;
        private const int INTEIRO_3 = 3;
        private const int NUMERO = 1000;
        private const int NUMERO_NEGATIVO = -100;
        private const int ANO = 1990;
        private const int ANO_NEGATIVO = -100;


        [Theory]
        [InlineData(TITULO, ARTISTA, NUMERO, true)]
        [InlineData(TITULO, ARTISTA, NUMERO_NEGATIVO, false)]
        public void RetornaTituloValidoQuandoDadosValidos(string p_titulo, string p_artista, int p_numero, bool p_validacao)
        {
            // Act
            Musica musica = new(p_titulo)
            {
                Artista = p_artista,
                Id = p_numero
            };

            // Assert
            Assert.Equal(TITULO, musica.Nome);
            Assert.Equal(p_validacao, musica.EstaPreenchidaTodosOsCamposCorretamente());
        }

        [Theory]
        [InlineData(MUSICA_TESTE, $"{NOME}{MUSICA_TESTE}")]
        [InlineData(OUTRA_MUSICA_TESTE, $"{NOME}{OUTRA_MUSICA_TESTE}")]
        [InlineData(MAIS_UMA_MUSICA_TESTE, $"{NOME}{MAIS_UMA_MUSICA_TESTE}")]
        public void ExibeDadosDaMusicaCorretamenteQuandoCamadoMetodoExibeDichaTecnica(string nome, string saidaesperada)
        {
            // Arrange
            Musica musica = new(nome);
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            // Act
            musica.ExibirFichaTecnica();
            string saidaAtual = stringWriter.ToString().Trim();

            //Assert
            Assert.Equal(saidaesperada, saidaAtual);
        }

        [Theory]
        [InlineData(INTEIRO_1, MUSICA_TESTE, $"{ID}{STRING_1} {NOME}{MUSICA_TESTE}")]
        [InlineData(INTEIRO_2, OUTRA_MUSICA_TESTE, $"{ID}{STRING_2} {NOME}{OUTRA_MUSICA_TESTE}")]
        [InlineData(INTEIRO_3, MAIS_UMA_MUSICA_TESTE, $"{ID}{STRING_3} {NOME}{MAIS_UMA_MUSICA_TESTE}")]
        public void ExibeDadosDaMusicaCorretamenteQuandoCahamdoMetodoToString(int id, string nome, string toStringEsperado)
        {
            // Arrange
            Musica musica = new(nome)
            {
                Id = id
            };

            // Act
            string resultado = musica.ToString();

            // Assert
            Assert.Equal(toStringEsperado, resultado);
        }

        [Fact]
        public void RetornaIDValidoQuandoIdForncidoCorretamente()
        {
            // Act
            Musica musica = new(TITULO)
            {
                Id = NUMERO,
                Artista = ARTISTA
            };

            //Asssert
            Assert.Equal(NUMERO, musica.Id);
        }

        [Fact]
        public void RetornaFuncaoToStringCorretamenteQuandoDadosFornecidosSaoValidos()
        {
            // Arrange
            string toStringEsperado = @$"{ID}{NUMERO} {NOME}{TITULO}";

            // Act
            Musica musica = new(TITULO)
            {
                Id = NUMERO,
                Artista = ARTISTA
            };

            // Assert
            Assert.Equal(toStringEsperado, musica.ToString());
        }

        [Fact]
        public void RentornaAnoDeLancamentoNuloQuandoValorEhMenorQueZero()
        {
            // Arrange
            int anoInvalido = -1;
            Musica musica = new(NOME)
            {
                // Act
                AnoLancamento = anoInvalido
            };

            // Assert
            Assert.Null(musica.AnoLancamento);
        }

        [Fact]
        public void RetornaArtistaDesconhecidoQuandoNuloOuEmBranco()
        {
            // Arrange
            Musica musica = new(NOME)
            {
                // Act
                Artista = string.Empty
            };

            // Assert
            Assert.Equal(Constantes.ARTISTA_DESCONHECIDO, musica.Artista);
        }
    }
}
