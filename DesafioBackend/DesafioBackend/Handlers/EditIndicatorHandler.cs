using DesafioBackend.Commands;
using DesafioBackend.DataBase;
using DesafioBackend.Indicators;
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
    public class EditIndicatorHandler : IRequestHandler<EditIndicatorCommand>
    {
        private readonly DesafioBackendContext _data;

        public EditIndicatorHandler(DesafioBackendContext data)
        {
            _data = data;
        }
        public async Task Handle(EditIndicatorCommand request, CancellationToken cancellationToken)
        {
            var selectedIndicator = await _data.Indicators.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (selectedIndicator == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            selectedIndicator.SetName(request.Name);
            selectedIndicator.SetResultType(request.ResultType);
            _data.Indicators.Update(selectedIndicator);
            await _data.SaveChangesAsync();
        }
    }
}
