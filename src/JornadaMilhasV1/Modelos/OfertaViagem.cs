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
            desconto = 0;
            PrecoOriginal = Preco;

            if ((value > 0) && (value < Preco))
            {
                desconto = value;
            } 
            else if (value >= Preco)
            {
                desconto = PrecoOriginal * DESCONTO_MAXIMO;
            }

            Preco -= desconto;
        }
    }
    public bool Ativa { get; set; } = true;


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
        try
        {
            if (!Periodo.EhValido)
            {
                Erros.RegistrarErro(Periodo.Erros.Sumario);
            }
        }
        catch (NullReferenceException)
        {
            Erros.RegistrarErro(Constantes.ERRO_PERIODO_NULO);
        } 
        if (Rota == null)
        {
            Erros.RegistrarErro(Constantes.ERRO_ROTA_INVALIDA);
        } 
        if (Preco <= 0)
        {
            Erros.RegistrarErro(Constantes.ERRO_PRECO_INVALIDO);
        }
    }
}
