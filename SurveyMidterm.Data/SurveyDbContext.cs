using Microsoft.EntityFrameworkCore;
using SurveyMidterm.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SurveyMidterm.Data
{
    public class SurveyDbContext : DbContext
    {
        public SurveyDbContext(DbContextOptions<SurveyDbContext> options) : base(options)
        {

        }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<SurveyUser> SurveyUsers { get; set; }
        public DbSet<Option> Options { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answer>(answer => {
                answer.Property(p => p.Id).IsRequired();
                answer.Property(p => p.UserId).IsRequired();
                answer.Property(p => p.OptionId).IsRequired();
                answer.HasKey(p => p.Id);
            });
            modelBuilder.Entity<Question>(question => {
                question.Property(p => p.Id).IsRequired();
                question.HasKey(p => p.Id);
                question.Property(p => p.Text).IsRequired();
                question.Property(p => p.Description).IsRequired();
            });
            modelBuilder.Entity<Option>(option => {
                option.Property(p => p.Id).IsRequired();
                option.HasKey(p => p.Id);
                option.Property(p => p.QuestionId).IsRequired();
                option.Property(p => p.Text).IsRequired();
            });
            modelBuilder.Entity<SurveyUser>(surveyUser => {
                surveyUser.Property(p => p.Id).IsRequired();
                surveyUser.HasKey(p => p.Id);
                surveyUser.Property(p => p.FirstName).IsRequired();
                surveyUser.Property(p => p.LastName).IsRequired();
                surveyUser.Property(p => p.DoB).IsRequired();
                surveyUser.Property(p => p.Country).IsRequired();
                surveyUser.Property(p => p.Gender).IsRequired();
            });
        }
    }
}
