using DesafioBackend.Commands;
using DesafioBackend.DataBase;
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
    public class DeleteIndicatorHandler : IRequestHandler<DeleteIndicatorCommand>
    {
        private readonly DesafioBackendContext _data;

        public DeleteIndicatorHandler(DesafioBackendContext data)
        {
            _data = data;
        }

        public async Task Handle(DeleteIndicatorCommand request, CancellationToken cancellationToken)
        {
            var selectedIndicator = await _data.IndicatorList.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (selectedIndicator == null)
                throw new InvalidOperationException("Indicator not found");
            _data.IndicatorList.Remove(selectedIndicator);
            await _data.SaveChangesAsync();
        }

    }
}
