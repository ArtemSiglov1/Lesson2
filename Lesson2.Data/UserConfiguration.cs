using Lesson2.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson2.Data
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User").Property(x => x.Name).IsRequired().HasMaxLength(80);

            builder.Property(x=>x.DateOfBirth).HasColumnType("datetime2");
        }
    }
}
