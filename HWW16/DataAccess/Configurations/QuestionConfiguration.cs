using HWW16.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HWW16.DataAccess.Configurations
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.HasData(
                
                new Question { Id = 1, SurveyId = 1, Text = "Are you satisfied with the quality of the course content?" },
                new Question { Id = 2, SurveyId = 1, Text = "Are you satisfied with the professor's teaching style?" },
                new Question { Id = 3, SurveyId = 2, Text = "Which backend language do you prefer?" }
            );
        }
    }
}