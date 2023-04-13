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
    public class DeleteDataCollectionPointHandler : IRequestHandler<DeleteDataCollectionPointCommand>
    {
        private readonly DesafioBackendContext _data;

        public DeleteDataCollectionPointHandler(DesafioBackendContext data)
        {
            _data = data;
        }
        public async Task Handle(DeleteDataCollectionPointCommand request, CancellationToken cancellationToken)
        {
            var selectedIndicator = await _data.IndicatorList
                .Include(x => x.DataCollectionPoints)
                .FirstOrDefaultAsync(x => x.Id == request.IndicatorId);
            if (selectedIndicator == null)
                throw new InvalidOperationException("404 NotFound");

            var selectedDCP = selectedIndicator.DataCollectionPoints
                .FirstOrDefault(dcp => dcp.Date == request.DataCollectionPointDate);
            if (selectedDCP == null)
                throw new InvalidOperationException("404 NotFound");

            selectedIndicator.DeleteDataCollectionPoint(request.DataCollectionPointDate);
            _data.IndicatorList.Update(selectedIndicator);
            await _data.SaveChangesAsync(cancellationToken);
        }
    }
}
