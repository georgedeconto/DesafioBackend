using DesafioBackend.DataCollection;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DesafioBackend.Indicators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioBackend.DataBase
{
    public class DataCollectionPointEntityTypeConfiguration : IEntityTypeConfiguration<DataCollectionPoint>
    {
        public void Configure(EntityTypeBuilder<DataCollectionPoint> builder)
        {
            builder
                .ToTable("DataCollectionPoints");

            builder
                .HasKey(dcp => dcp.Id);

            builder
                .Property(dcp => dcp.Id)
                .ValueGeneratedNever();

            builder
                .HasIndex(dcp => dcp.Date);

            builder
                .Property(dcp => dcp.Date)
                .IsRequired();

            builder
                .Property(dcp => dcp.Value)
                .IsRequired();

            builder
                .HasOne(dcp => dcp.Indicator)
                .WithMany(dcp => dcp.DataCollectionPoints)
                .HasForeignKey(dcp => dcp.IndicatorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
