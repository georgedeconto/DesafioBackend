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
    public class AddDataCollectionPointHandler : IRequestHandler<AddDataCollectionPointCommand>
    {
        private readonly DesafioBackendContext _data;

        public AddDataCollectionPointHandler(DesafioBackendContext data)
        {
            _data = data;
        }
        public async Task Handle(AddDataCollectionPointCommand request, CancellationToken cancellationToken)
        {
            var selectedIndicator = await _data.IndicatorList
                .Include(d => d.DataCollectionPoints)
                .FirstOrDefaultAsync(x => x.Id == request.Id);
            if (selectedIndicator == null)
                throw new InvalidOperationException("404 NotFound");
            selectedIndicator.AddDataCollectionPoint(request.Date, request.Value);
            _data.IndicatorList.Update(selectedIndicator);
            await _data.SaveChangesAsync();
        }
    }
}
