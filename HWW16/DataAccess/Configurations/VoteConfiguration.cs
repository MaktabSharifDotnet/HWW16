using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class VoteConfiguration : IEntityTypeConfiguration<Vote>
{
    public void Configure(EntityTypeBuilder<Vote> builder)
    {
        builder.HasOne(v => v.Question)
               .WithMany(q => q.Votes)
               .HasForeignKey(v => v.QuestionId)
               .OnDelete(DeleteBehavior.ClientSetNull); 

        builder.HasOne(v => v.SelectedOption)
               .WithMany(o => o.Votes)
               .HasForeignKey(v => v.SelectedOptionId)
               .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(v => v.Survey)
               .WithMany(s => s.Votes)
               .HasForeignKey(v => v.SurveyId)
               .OnDelete(DeleteBehavior.Cascade); 

        builder.HasOne(v => v.User)
               .WithMany(u => u.Votes)
               .HasForeignKey(v => v.UserId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}