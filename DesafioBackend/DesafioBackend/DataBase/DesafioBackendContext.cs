using DesafioBackend.DataCollection;
using DesafioBackend.Indicators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DesafioBackend.DataBase
{
    public class DesafioBackendContext : DbContext
    {
        public DesafioBackendContext(DbContextOptions<DesafioBackendContext> options)
            : base(options)
        {
        }

        public DbSet<Indicator> Indicators => Set<Indicator>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
