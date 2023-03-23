using DesafioBackend.Coletas;
using DesafioBackend.Indicators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace DesafioBackend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<Coleta> lista = new List<Coleta>();
            Coleta coleta1 = new(DateTime.Today, 100.1);
            Coleta coleta2 = new(DateTime.Today, 200);
            lista.Add(coleta1);
            lista.Add(coleta2);
            Indicator indic = new("nome", lista, EnumResultado.Media);
            Console.WriteLine(indic.Coletas[0].Valor);
            indic.EditColeta(indic.Coletas[0].Id, 1);
            Console.WriteLine(indic.Coletas[0].Valor);
        }


    }
}