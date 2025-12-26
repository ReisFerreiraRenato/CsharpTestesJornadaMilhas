using JornadaMilhas.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JornadaMilhas.Gerenciador;
public class GerenciadorDeOfertas
{
    private readonly List<OfertaViagem> ofertaViagem = new();

    public GerenciadorDeOfertas(List<OfertaViagem> ofertaViagem)
    {
        this.ofertaViagem = ofertaViagem;
    }

    public void CadastrarOferta()
    {
        Console.WriteLine("-- Cadastro de ofertas --");
        Console.WriteLine("Informe a cidade de origem: ");
        string origem = Console.ReadLine()!;


        Console.WriteLine("Informe a cidade de destino: ");
        string destino = Console.ReadLine()!;

        Console.WriteLine("Informe a data de ida (DD/MM/AAAA): ");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime dataIda))
        {
            Console.WriteLine("Data de ida inválida.");
            return;
        }

        Console.WriteLine("Informe a data de volta (DD/MM/AAAA): ");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime dataVolta))
        {
            Console.WriteLine("Data de volta inválida.");
            return;
        }

        Console.WriteLine("Informe o preço: ");
        if (!double.TryParse(Console.ReadLine(), out double preco))
        {
            Console.WriteLine("Formato de preço inválido.");
            return;
        }

        OfertaViagem ofertaCadastrada = new(new Rota(origem, destino), new Periodo(dataIda, dataVolta), preco);
        AdicionarOfertaNaLista(ofertaCadastrada);

        Console.WriteLine("\nOferta cadastrada com sucesso.");
    }

    public bool AdicionarOfertaNaLista(OfertaViagem ofertaCadastrada)
    {
        if (ofertaCadastrada != null)
        {
            ofertaViagem.Add(ofertaCadastrada);
            return true;
        }
        return false;

    }

    public OfertaViagem? RecuperaMaiorDesconto(Func<OfertaViagem, bool> filtro) => ofertaViagem.FirstOrDefault();

    public void CarregarOfertas()
    {
        AdicionarOfertaNaLista(new OfertaViagem(new Rota("São Paulo", "Curitiba"), new Periodo(new DateTime(2024, 1, 15), new DateTime(2024, 1, 20)), 500));
        AdicionarOfertaNaLista(new OfertaViagem(new Rota("Recife", "Rio de Janeiro"), new Periodo(new DateTime(2024, 2, 10), new DateTime(2024, 2, 15)), 700));
        AdicionarOfertaNaLista(new OfertaViagem(new Rota("Acre", "Brasília"), new Periodo(new DateTime(2024, 3, 5), new DateTime(2024, 3, 10)), 600));
    }

    public void ExibirTodasAsOfertas()
    {
        Console.WriteLine("\nTodas as ofertas cadastradas: ");
        foreach (var oferta in ofertaViagem)
        {
            Console.WriteLine(oferta);
        }
    }
}
