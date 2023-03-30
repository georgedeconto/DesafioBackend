namespace DesafioBackend.DataCollection
{
    public class DataCollectionPoint
    {
        public Guid Id { get; }
        public Guid IndicatorId { get; }
        public DateTime Date { get; }
        public double Value { get; private set; }

        public DataCollectionPoint(Guid indicatorId, DateTime date, double value)
        {
            if (date > DateTime.Now)
                throw new ArgumentException("Data não pode ser no futuro");

            Id = Guid.NewGuid();
            IndicatorId = indicatorId;
            Date = date;
            Value = value;
        }

        public void SetValue(double value)
        {
            Value = value;
        }
    }
}