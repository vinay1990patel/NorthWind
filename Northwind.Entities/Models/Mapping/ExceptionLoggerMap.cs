using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations;

namespace Northwind.Entities.Models.Mapping
{
  public  class ExceptionLoggerMap:EntityTypeConfiguration<ExceptionLogger>
    {
        public ExceptionLoggerMap()
        {
            // Primary Key
            this.HasKey(p => p.id);
              
            // Properties
            this.Property(p => p.ExceptionMessage)
                .IsRequired()
                .HasMaxLength(50);
            this.Property(k => k.ExceptionStackTrace)
                .HasMaxLength(50);
            this.Property(l => l.RequestMethod)
                .HasMaxLength(50);
            this.Property(d => d.LogTime)
                .IsOptional();
            this.Property(c => c.ControllerName)
                .IsRequired()
                .HasMaxLength(50);
              
        }
    }
}
