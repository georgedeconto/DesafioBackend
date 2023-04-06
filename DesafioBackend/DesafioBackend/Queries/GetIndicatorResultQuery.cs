using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioBackend.Queries
{
    public record GetIndicatorResultQuery(Guid Id) : IRequest<double>;
}
