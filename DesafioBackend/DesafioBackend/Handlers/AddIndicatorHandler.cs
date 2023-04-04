using DesafioBackend.Commands;
using DesafioBackend.DataBase;
using DesafioBackend.Indicators;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DesafioBackend.Handlers
{
    public class AddIndicatorHandler : IRequestHandler<AddIndicatorCommand>
    {
        private readonly DesafioBackendContext _data;

        public AddIndicatorHandler(DesafioBackendContext data)
        {
            _data = data;
        }
        public async Task Handle(AddIndicatorCommand request, CancellationToken cancellationToken)
        {

            var newIndicator = new Indicator(request.Name, request.ResultType);
            await _data.IndicatorList.AddAsync(newIndicator);
            await _data.SaveChangesAsync();
        }
    }
}
