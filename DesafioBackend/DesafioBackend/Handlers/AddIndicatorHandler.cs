using DesafioBackend.Commands;
using DesafioBackend.DataBase;
using DesafioBackend.Indicators;
using MediatR;
using System;
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
            if (string.IsNullOrWhiteSpace(request.Name))
                throw new ArgumentException("Name cannot be empty or white space");

            if (request.ResultType.GetType() != typeof(EnumResult))
                throw new ArgumentException("Invalid ResultType");

            var newIndicator = new Indicator(request.Name, request.ResultType);
            await _data.Indicators.AddAsync(newIndicator);
            await _data.SaveChangesAsync();
        }
    }
}
