using DesafioBackend.Coletas;

namespace DesafioBackend.Indicators
{

    public enum EnumResultado
    {
        Soma,
        Media
    }

    public class Indicator
    {
        public Guid Id { get; init; }
        public string Nome { get; private set; }
        public EnumResultado Resultado { get; set; }
        public List<Coleta> Coletas { get; private set; }

        public Indicator(string nome, List<Coleta> coletas, EnumResultado resultado)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("O nome deve ser preenchido");

            Id = Guid.NewGuid();
            Nome = nome;
            Coletas = coletas;
            Resultado = resultado;
        }

        public void SetNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("O nome deve ser preenchido");
            Nome = nome;
        }

        public void AddColeta(Coleta coleta)
        {
            Coletas.Add(coleta);
        }

        public void DeleteColeta(Guid id)
        {
            var coleta = Coletas.FirstOrDefault(c => c.Id == id);
            if (coleta == null)
                throw new ArgumentException("Coleta não encontrada", nameof(id));

            Coletas.Remove(coleta);
        }

        public void EditColeta(Guid id, double valor)
        {
            var coleta = Coletas.FirstOrDefault(c => c.Id == id);
            if (coleta == null)
                throw new ArgumentException("Coleta não encontrada", nameof(id));

            coleta.SetValor(valor);
        }

        public double CalcularResultado()
        {
            if (Coletas.Count != 0)
            {
                var arrayValor = Coletas.Select(x => x.Valor);
                EnumResultado resultado = this.Resultado;
                switch (resultado)
                {
                    case EnumResultado.Soma:
                        return arrayValor.Sum();
                    case EnumResultado.Media:
                        return arrayValor.Average();
                }
            }
            throw new InvalidOperationException("Não há coletas neste indicador");
        }
    }

}

