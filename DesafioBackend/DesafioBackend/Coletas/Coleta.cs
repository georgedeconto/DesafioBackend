namespace DesafioBackend.Coletas
{
    public class Coleta
    {
        public Guid Id { get; }
        public DateTime Date { get; }

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
    }
}