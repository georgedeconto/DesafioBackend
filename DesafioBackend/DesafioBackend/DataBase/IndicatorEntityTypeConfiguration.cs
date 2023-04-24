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
                .ToTable("Indicators");

            builder
                .HasKey(i => i.Id);

            builder
                .Property(i => i.Id)
                .ValueGeneratedNever();

            builder
                .Property(i => i.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder
                .Property(i => i.ResultType)
                .IsRequired();
        }
    }
}
