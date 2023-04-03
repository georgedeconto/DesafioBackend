using DesafioBackend.DataCollection;
using System.Xml.Linq;

namespace DesafioBackend.Indicators
{
    public class Indicator
    {
        public Guid Id { get; }
        public string Name { get; private set; } = null!;
        public EnumResult ResultType { get; private set; }
        public List<DataCollectionPoint> DataCollectionPoints { get; private set; }

        public Indicator(string name, EnumResult resultType)
        {
            Id = Guid.NewGuid();
            SetName(name);
            DataCollectionPoints = new List<DataCollectionPoint>();
            ResultType = resultType;
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("O nome deve ser preenchido");
            Name = name;
        }

        public void SetResultType(EnumResult resultType)
        {
            ResultType = resultType;
        }

        public void AddDataCollectionPoint(DateTime date, double value)
        {
            var dataCollectionPoint = DataCollectionPoints.FirstOrDefault(c => c.Date == date);
            if (dataCollectionPoint != null)
                throw new ArgumentException("Já existe uma coleta nesta data");

            var newPoint = new DataCollectionPoint(Id, date: date, value: value);
            DataCollectionPoints.Add(newPoint);
        }

        public void DeleteDataCollectionPoint(DateTime date)
        {
            var dataCollectionPoint = DataCollectionPoints.FirstOrDefault(c => c.Date == date);
            if (dataCollectionPoint == null)
                throw new ArgumentException("Coleta não encontrada", nameof(date));

            DataCollectionPoints.Remove(dataCollectionPoint);
        }

        public void EditDataCollectionPoint(DateTime date, double value)
        {
            var dataCollectionPoint = DataCollectionPoints.FirstOrDefault(c => c.Date == date);
            if (dataCollectionPoint == null)
                throw new ArgumentException("Coleta não encontrada", nameof(date));

            dataCollectionPoint.SetValue(value);
        }

        public double CalculateResult()
        {
            if (DataCollectionPoints.Count != 0)
            {
                var valueArray = DataCollectionPoints.Select(x => x.Value);
                switch (ResultType)
                {
                    case EnumResult.Sum:
                        return valueArray.Sum();
                    case EnumResult.Average:
                        return valueArray.Average();
                }
            }
            throw new InvalidOperationException("Não há coletas neste indicador");
        }
    }

}

