using DesafioBackend.Commands;
using DesafioBackend.DataBase;
using DesafioBackend.Indicators;
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
    public class EditIndicatorHandler : IRequestHandler<EditIndicatorCommand>
    {
        private readonly DesafioBackendContext _data;

        public EditIndicatorHandler(DesafioBackendContext data)
        {
            _data = data;
        }
        public async Task Handle(EditIndicatorCommand request, CancellationToken cancellationToken)
        {
            var selectedIndicator = await _data.IndicatorList.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (selectedIndicator == null)
                throw new InvalidOperationException("404 NotFound");
            selectedIndicator.SetName(request.Name);
            await _data.SaveChangesAsync();
        }
    }
}
