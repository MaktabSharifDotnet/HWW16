using HWW16.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWW16.DataAccess
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Vote> Votes { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer(
              "Server=DESKTOP-M2BLLND\\SQLEXPRESS;Database=HWW16;Integrated Security=True;TrustServerCertificate=True;Encrypt=False;"
              );

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 

          
            modelBuilder.Entity<Vote>()
                .HasOne(v => v.Question) 
                .WithMany(q => q.Votes) 
                .HasForeignKey(v => v.QuestionId) 
                .OnDelete(DeleteBehavior.Restrict); 

         
            modelBuilder.Entity<Vote>()
                .HasOne(v => v.SelectedOption)
                .WithMany(o => o.Votes)
                .HasForeignKey(v => v.SelectedOptionId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Vote>()
               .HasOne(v => v.Survey)
               .WithMany(s => s.Votes)
               .HasForeignKey(v => v.SurveyId)
               .OnDelete(DeleteBehavior.Restrict); 

          
        }
    }
}





