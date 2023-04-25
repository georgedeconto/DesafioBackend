using DesafioBackend.DataBase;
using DesafioBackend.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace DesafioBackend.Handlers
{
    public class GetIndicatorResultHandler : IRequestHandler<GetIndicatorResultQuery, double>
    {
        private readonly DesafioBackendContext _data;

        public GetIndicatorResultHandler(DesafioBackendContext data)
        {
            _data = data;
        }
        public async Task<double> Handle(GetIndicatorResultQuery request, CancellationToken cancellationToken)
        {
            var selectedIndicator = await _data.Indicators
                .Include(x => x.DataCollectionPoints)
                .FirstOrDefaultAsync(x => x.Id == request.Id);
            if (selectedIndicator == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return selectedIndicator.CalculateResult();
        }
    }
}
