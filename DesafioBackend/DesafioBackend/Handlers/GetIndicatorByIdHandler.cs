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
    internal class GetIndicatorByIdHandler : IRequestHandler<GetIndicatorByIdQuery, IndicatorViewModel>
    {
        private readonly DesafioBackendContext _data;

        public GetIndicatorByIdHandler(DesafioBackendContext data)
        {
            _data = data;
        }
        public async Task<IndicatorViewModel> Handle(GetIndicatorByIdQuery request, CancellationToken cancellationToken)
        {
            var output = await _data.IndicatorList.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (output == null)
                throw new InvalidOperationException("Indicator not found");
            return new IndicatorViewModel(output);
        }
    }
}
