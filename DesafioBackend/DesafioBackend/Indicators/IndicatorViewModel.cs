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
        public Guid Id { get;  }
        public string Name { get;  }
        public EnumResult ResultType { get; }
        public List<DataCollectionPointViewModel> DataCollectionPoints { get; }

        public IndicatorViewModel(Indicator indicator)
        {
            Id = indicator.Id;
            Name = indicator.Name;
            ResultType = indicator.ResultType;
            DataCollectionPoints = new List<DataCollectionPointViewModel>(indicator.DataCollectionPoints.Select(d => new DataCollectionPointViewModel(d)));
        }
    }
}
