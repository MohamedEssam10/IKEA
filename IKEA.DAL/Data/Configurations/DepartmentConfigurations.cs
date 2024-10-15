using IKEA.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Data.Configurations
{
    internal class DepartmentConfigurations : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            // HasComputedColumnSql(); Executes GETDATE() every time the recored is modified
            builder.Property(D => D.LastModifiedOn).HasComputedColumnSql("GETDATE()");
            builder.Property(d => d.CreatedOn).HasDefaultValueSql("GETDATE()");
        }
    }
}
