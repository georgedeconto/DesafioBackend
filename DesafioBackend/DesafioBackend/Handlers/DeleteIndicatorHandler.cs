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
    public class DeleteIndicatorHandler : IRequestHandler<DeleteIndicatorCommand>
    {
        private readonly DesafioBackendContext _data;

        public DeleteIndicatorHandler(DesafioBackendContext data)
        {
            _data = data;
        }

        public async Task Handle(DeleteIndicatorCommand request, CancellationToken cancellationToken)
        {
            var selectedIndicator = await _data.Indicators.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (selectedIndicator == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            _data.Indicators.Remove(selectedIndicator);
            await _data.SaveChangesAsync();
        }

    }
}
