using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JornadaMilhas.Utils
{
    public static class Constantes
    {
        //Constantes de testes
        public const string ORIGEM_TESTE = "Origem Teste";
        public const string DESTINO_TESTE = "Destino Teste";
        public const string ARTISTA_DESCONHECIDO = "Artista Desconhecido.";

        public const string STRING_NULA = null;
        public const double DOUBLE_NEGATIVO = - 1000.0;


        //Cosntantes gerais (podem inclusive serem de testes também)
        public const string ERRO_ROTA_INVALIDA = "A oferta de viagem não possui rota válida.";
        public const string ERRO_PERIODO_INVALIDA = "A oferta de viagem não possui período válido.";
        public const string ERRO_PRECO_INVALIDO = "O preço da oferta de viagem deve ser maior que zero.";
        public const string ERRO_PERIODO_NULO = "A oferta de viagem possui período nulo.";
        public const string ERRO_DATAS_INVALIDAS = "Erro: Data de ida não pode ser maior que a data de volta.";
    }
}
