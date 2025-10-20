using HWW16.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HWW16.DataAccess.Configurations
{
    public class SurveyConfiguration : IEntityTypeConfiguration<Survey>
    {
        public void Configure(EntityTypeBuilder<Survey> builder)
        {
            builder.HasData(
                new Survey { Id = 1, Title = "Bootcamp satisfaction level", CreatorUserId = 1 }, 
                new Survey { Id = 2, Title = "Favorite programming language", CreatorUserId = 1 } 
            );
        }
    }
}