using DesafioBackend.Indicators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioBackend.DataCollection
{
    public struct DataCollectionPointViewModel
    {
        public Guid Id { get; }
        public Guid IndicatorId { get; }
        public DateTime Date { get; }
        public double Value { get; }

        public DataCollectionPointViewModel(DataCollectionPoint dataCollectionPoint)
        {
            Id = dataCollectionPoint.Id;
            IndicatorId = dataCollectionPoint.IndicatorId;
            Date = dataCollectionPoint.Date;
            Value = dataCollectionPoint.Value;
        }
    }
}
