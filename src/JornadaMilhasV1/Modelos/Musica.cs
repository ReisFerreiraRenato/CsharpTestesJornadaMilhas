using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JornadaMilhas.Utils;
using Microsoft.IdentityModel.Tokens;

namespace JornadaMilhas.Modelos
{
    public class Musica
    {
        private int? anoLancamento;
        private string artista;

        public string Nome { get; set; }
        public int Id { get; set; }
        public string Artista
        {
            get => artista;
            set
            {
                artista = value;
                if (value.IsNullOrEmpty())
                {
                    artista = Constantes.ARTISTA_DESCONHECIDO;
                }
            }
        }

        public int? AnoLancamento
        {
            get => anoLancamento;
            set
            {
                anoLancamento = null;
                if (value > 0)
                    anoLancamento = value;
            }
        }

        public Musica(string nome)
        {
            Nome = nome;
        }

        public void ExibirFichaTecnica()
        {
            Console.WriteLine($"Nome: {Nome}");
            //Console.WriteLine($"Artista: {Artista}");
            //Console.WriteLine($"ID: {Id}");
        }

        public override string ToString()
        {
            return @$"Id: {Id} Nome: {Nome}";
        }

        public bool EstaPreenchidaTodosOsCamposCorretamente()
        {
            if (string.IsNullOrEmpty(Nome)) return false;
            if (string.IsNullOrEmpty(Artista)) return false;
            if (Id <= 0) return false;

            return true;
        }
    }
}
