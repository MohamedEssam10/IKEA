using IKEA.DAL.Common.Enums;
using IKEA.DAL.Entities.Employees;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Data.Configurations
{
    internal class EmployeeConfigurations : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(E => E.Gender).
                HasConversion
                (
                    gender => gender.ToString(),
                    gender => (Gender) Enum.Parse(typeof(Gender),gender)
                );

            builder.Property(E => E.EmployeeType)
                .HasConversion
                (
                    EmployeeType => EmployeeType.ToString(),
                    EmployeeType => (EmployeeType)Enum.Parse(typeof(EmployeeType), EmployeeType)
                );
            builder.Property(E => E.CreatedOn).HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
