
using HWW16.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HWW16.DataAccess.Configurations
{
    public class OptionConfiguration : IEntityTypeConfiguration<Option>
    {
        public void Configure(EntityTypeBuilder<Option> builder)
        {
            builder.HasData(
               
                new Option { Id = 1, QuestionId = 1, Text = "Completely satisfied" },
                new Option { Id = 2, QuestionId = 1, Text = "Satisfied" },
                new Option { Id = 3, QuestionId = 1, Text = "No opinion" },
                new Option { Id = 4, QuestionId = 1, Text = "Dissatisfied" },

                new Option { Id = 5, QuestionId = 2, Text = "Completely satisfied" },
                new Option { Id = 6, QuestionId = 2, Text = "Satisfied" },
                new Option { Id = 7, QuestionId = 2, Text = "No opinion" },
                new Option { Id = 8, QuestionId = 2, Text = "Dissatisfied" },

                
                new Option { Id = 9, QuestionId = 3, Text = "C#" },
                new Option { Id = 10, QuestionId = 3, Text = "Java" },
                new Option { Id = 11, QuestionId = 3, Text = "Python" },
                new Option { Id = 12, QuestionId = 3, Text = "PHP" }
            );
        }
    }
}