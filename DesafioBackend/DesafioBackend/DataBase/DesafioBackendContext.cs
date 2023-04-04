using DesafioBackend.DataCollection;
using DesafioBackend.Indicators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioBackend.DataBase
{
    public class DesafioBackendContext : DbContext
    {
        public DbSet<Indicator> IndicatorList => Set<Indicator>();
        //public DbSet<DataCollectionPoint> DataCollectionPointList => Set<DataCollectionPoint>();
        public DesafioBackendContext(DbContextOptions<DesafioBackendContext> options)
            : base(options)
        {
        }
    }
}
