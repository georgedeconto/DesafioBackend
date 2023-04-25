using DesafioBackend.DataBase;
using DesafioBackend.Indicators;
using DesafioBackend.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace DesafioBackend.Handlers
{
    public class GetIndicatorByIdHandler : IRequestHandler<GetIndicatorByIdQuery, IndicatorViewModel>
    {
        private readonly DesafioBackendContext _data;

        public GetIndicatorByIdHandler(DesafioBackendContext data)
        {
            _data = data;
        }
        public async Task<IndicatorViewModel> Handle(GetIndicatorByIdQuery request, CancellationToken cancellationToken)
        {
            var output = await _data.Indicators
                .Include(d => d.DataCollectionPoints)
                .FirstOrDefaultAsync(x => x.Id == request.Id);
            if (output == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return new IndicatorViewModel(output);
        }
    }
}
