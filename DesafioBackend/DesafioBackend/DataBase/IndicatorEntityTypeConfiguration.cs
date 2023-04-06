using DesafioBackend.Indicators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioBackend.DataBase
{
    public class IndicatorEntityTypeConfiguration : IEntityTypeConfiguration<Indicator>
    {
        public void Configure(EntityTypeBuilder<Indicator> builder)
        {
            builder
                .ToTable("IndicatorList");

            builder
                .HasKey(k => k.Id);

            builder
                .Property(k => k.Id)
                .ValueGeneratedNever();

            builder
                .Property(k => k.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder
                .Property(k => k.ResultType)
                .IsRequired();
        }
    }
}
