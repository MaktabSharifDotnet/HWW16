using HWW16.Entities;
using HWW16.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWW16.DataAccess.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {

        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.HasData(
                new User { Id = 1, Username = "admin", Password = "123", Role = RoleEnum.Admin },
                new User { Id = 2, Username = "user1", Password = "123", Role = RoleEnum.NormalUser }
            );
        }
    }

}
