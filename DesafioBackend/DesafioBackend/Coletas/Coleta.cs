namespace DesafioBackend.Coletas
{
    public class Coleta
    {
        public Guid Id { get; init; }
        public DateTime Date { get; init; }

        public double Valor { get; private set; }


        public Coleta(DateTime date, double valor)
        {
            if (date > DateTime.Now)
                throw new ArgumentException("Data não pode ser no futuro");

            Id = Guid.NewGuid();
            Date = date;
            Valor = valor;
        }

        public void SetValor(double valor)
        {
            Valor = valor;
        }

        /*
        public void SetDate(Coleta coleta, DateTime newDate)
        {
            if (newDate <= DateTime.Today)
                throw new InvalidOperationException("Data não pode ser no futuro");
                coleta.Date = newDate;
            }
        }
        */
    }
}