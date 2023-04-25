using DesafioBackend.Commands;
using DesafioBackend.DataBase;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace DesafioBackend.Handlers
{
    public class DeleteDataCollectionPointHandler : IRequestHandler<DeleteDataCollectionPointCommand>
    {
        private readonly DesafioBackendContext _data;

        public DeleteDataCollectionPointHandler(DesafioBackendContext data)
        {
            _data = data;
        }
        public async Task Handle(DeleteDataCollectionPointCommand request, CancellationToken cancellationToken)
        {
            var selectedIndicator = await _data.Indicators
                .Include(x => x.DataCollectionPoints)
                .FirstOrDefaultAsync(x => x.Id == request.IndicatorId);
            if (selectedIndicator == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var selectedDCP = selectedIndicator.DataCollectionPoints
                .FirstOrDefault(dcp => dcp.Date == request.DataCollectionPointDate);
            if (selectedDCP == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            selectedIndicator.DeleteDataCollectionPoint(request.DataCollectionPointDate);
            _data.Indicators.Update(selectedIndicator);
            await _data.SaveChangesAsync(cancellationToken);
        }
    }
}
