using Microsoft.EntityFrameworkCore;
using OnlineLearningPlatform.Models;
using System.Collections.Generic;

namespace OnlineLearningPlatform.Data
{
    public class LearningPlatformContext : DbContext
    {
        public LearningPlatformContext(DbContextOptions<LearningPlatformContext> options) : base(options) { }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<StudentProgress> StudentProgresses { get; set; }
    }
}
