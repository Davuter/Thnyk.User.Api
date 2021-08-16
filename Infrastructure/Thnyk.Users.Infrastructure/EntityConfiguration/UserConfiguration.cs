using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Thnyk.Core.Domain.Entities;

namespace Thnyk.Users.Infrastructure.EntityConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).UseIdentityColumn();
            builder.Property(i => i.Name).IsRequired();
            builder.Property(i => i.Job).IsRequired();
            builder.Property(i => i.CreatedDate).IsRequired();
            builder.Property(i => i.UserPicture).IsRequired();
        }
    }
}
