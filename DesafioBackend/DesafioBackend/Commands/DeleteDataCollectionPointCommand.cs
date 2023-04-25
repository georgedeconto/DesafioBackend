using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioBackend.Commands
{
    public record DeleteDataCollectionPointCommand(Guid IndicatorId, DateTime DataCollectionPointDate) : IRequest;
}
