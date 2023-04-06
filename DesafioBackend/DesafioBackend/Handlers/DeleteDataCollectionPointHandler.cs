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
                throw new InvalidOperationException("Indicator not found");

            var selectedDCP = selectedIndicator.DataCollectionPoints
                .FirstOrDefault(dcp => dcp.Date == request.DCPDate);
            if (selectedDCP == null)
                throw new InvalidOperationException("Data Collection Point not found");

            selectedIndicator.DataCollectionPoints.Remove(selectedDCP);
            _data.Update(selectedIndicator);
            await _data.SaveChangesAsync(cancellationToken);
        }
    }
}
