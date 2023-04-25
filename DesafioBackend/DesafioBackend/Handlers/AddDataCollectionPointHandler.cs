using DesafioBackend.Commands;
using DesafioBackend.DataBase;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace DesafioBackend.Handlers
{
    public class AddDataCollectionPointHandler : IRequestHandler<AddDataCollectionPointCommand>
    {
        private readonly DesafioBackendContext _data;

        public AddDataCollectionPointHandler(DesafioBackendContext data)
        {
            _data = data;
        }
        public async Task Handle(AddDataCollectionPointCommand request, CancellationToken cancellationToken)
        {
            var selectedIndicator = await _data.Indicators
                .Include(d => d.DataCollectionPoints)
                .FirstOrDefaultAsync(x => x.Id == request.Id);
            if (selectedIndicator == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            selectedIndicator.AddDataCollectionPoint(request.Date, request.Value);
            _data.Indicators.Update(selectedIndicator);
            await _data.SaveChangesAsync();
        }
    }
}
