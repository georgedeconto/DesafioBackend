using DesafioBackend.Indicators;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioBackend.Commands
{
    public record AddIndicatorCommand(string Name, EnumResult ResultType) : IRequest;
}
