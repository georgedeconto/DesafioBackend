using DesafioBackend.DataBase;
using DesafioBackend.Indicators;
using DesafioBackend.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DesafioBackend.Handlers
{
    public class GetIndicatorListHandler : IRequestHandler<GetIndicatorListQuery, List<IndicatorViewModel>>
    {
        private readonly DesafioBackendContext _data;

        public GetIndicatorListHandler(DesafioBackendContext data)
        {
            _data = data;
        }
        public async Task<List<IndicatorViewModel>> Handle(GetIndicatorListQuery request, CancellationToken cancellationToken)
        {
            var indicatorList = await _data.Indicators
                .Include(d => d.DataCollectionPoints)
                .AsNoTracking()
                .Select(i => new IndicatorViewModel(i))
                .ToListAsync(cancellationToken);

            return indicatorList;
        }
    }
}
