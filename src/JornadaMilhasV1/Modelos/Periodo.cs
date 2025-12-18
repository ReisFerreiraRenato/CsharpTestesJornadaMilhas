using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JornadaMilhas.Utils;
using JornadaMilhas.Validador;

namespace JornadaMilhas.Modelos;

public class Periodo: Valida
{
    public DateTime DataInicial { get; set; }
    public DateTime DataFinal { get; set; }

    public Periodo(DateTime dataInicial, DateTime dataFinal)
    {
        DataInicial = dataInicial;
        DataFinal = dataFinal;
        Validar();
    }

    protected override void Validar()
    {
        if (DataInicial > DataFinal)
        {
            Erros.RegistrarErro(Constantes.ERRO_DATAS_INVALIDAS);
        }
    }
}
