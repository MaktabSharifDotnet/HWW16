using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWW16.DataAccess.Configurations
{
    public class VoteConfiguration : IEntityTypeConfiguration<Vote>
    {
        public void Configure(EntityTypeBuilder<Vote> builder)
        {
            builder.HasOne(v => v.Question)
                   .WithMany(q => q.Votes)
                   .HasForeignKey(v => v.QuestionId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(v => v.SelectedOption)
                   .WithMany(o => o.Votes)
                   .HasForeignKey(v => v.SelectedOptionId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(v => v.Survey)
                   .WithMany(s => s.Votes)
                   .HasForeignKey(v => v.SurveyId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
