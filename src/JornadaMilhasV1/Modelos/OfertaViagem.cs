using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JornadaMilhas.Validador;
using JornadaMilhas.Utils;

namespace JornadaMilhas.Modelos;

public class OfertaViagem: Valida
{
    public const double DESCONTO_MAXIMO = 0.7;
    private double desconto;

    public int Id { get; set; }
    public Rota Rota { get; set; } 
    public Periodo Periodo { get; set; }
    public double Preco { get; set; }
    public double PrecoOriginal { get; set; }
    public double Desconto 
    { 
        get => desconto;
        set
        {
            desconto = value;
            PrecoOriginal = Preco;
            if (desconto >= Preco)
            {
                desconto = Preco * DESCONTO_MAXIMO;
                Preco *= (1 - DESCONTO_MAXIMO);
            }
            else if (desconto < 0)
            {
                desconto = 0;
            }
            else
            {
                Preco -= desconto;
            }
        }
    }


    public OfertaViagem(Rota rota, Periodo periodo, double preco)
    {
        Rota = rota;
        Periodo = periodo;
        Preco = preco;
        Validar();
    }

    public override string ToString()
    {
        return $"Origem: {Rota.Origem}, Destino: {Rota.Destino}, Data de Ida: {Periodo.DataInicial:d}, Data de Volta: {Periodo.DataFinal:d}, Preço: {Preco:C}";
    }

    protected override void Validar()
    {
        if (Periodo == null)
        {
            Erros.RegistrarErro(Constantes.ERRO_PERIODO_NULO);
        }
        else if (!Periodo.EhValido)
        {
            Erros.RegistrarErro(Periodo.Erros.Sumario);
        } else if (Rota == null)
        {
            Erros.RegistrarErro(Constantes.ERRO_ROTA_INVALIDA);
        } 
        else if (Preco <= 0)
        {
            Erros.RegistrarErro(Constantes.ERRO_PRECO_INVALIDO);
        }
    }
}
