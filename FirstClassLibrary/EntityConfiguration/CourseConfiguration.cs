using FirstClassLibrary.Entity;
using Microsoft.EntityFrameworkCore;
using FirstClassLibrary.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FirstClassLibrary.EntityConfiguration
{
    public class CourseConfiguration:IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("Course");
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

           // throw new NotImplementedException();
        }
    }
}
