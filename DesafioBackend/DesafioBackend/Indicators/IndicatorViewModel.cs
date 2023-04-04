using DesafioBackend.DataCollection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioBackend.Indicators
{
    public struct IndicatorViewModel
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public EnumResult ResultType { get; init; }
        public List<DataCollectionPoint> DataCollectionPoints { get; init; }

        public IndicatorViewModel(Indicator indicator)
        {
            Id = indicator.Id;
            Name = indicator.Name;
            ResultType = indicator.ResultType;
            DataCollectionPoints = indicator.DataCollectionPoints;
        }
    }
}
