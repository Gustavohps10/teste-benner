using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using microwave_benner.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace microwave_benner.Infra.Data.EntitiesConfig
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.HasKey(u => u.id);
            builder.Property(u => u.username)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(u => u.password)
                .IsRequired()
                .HasMaxLength(256);

            builder.ToTable("users");
        }
    }
}
