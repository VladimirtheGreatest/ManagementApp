using Microsoft.EntityFrameworkCore;
using TranslationManagement.Data.Entities;

namespace TranslationManagement.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<TranslationJob> TranslationJobs { get; set; }
        public DbSet<TranslatorModel> Translators { get; set; }
        public DbSet<TranslatorStatus> TranslatorStatuses { get; set; }
        public DbSet<TranslationJobStatus> TranslatorJobStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TranslatorStatus>().HasData(
            new TranslatorStatus { Id = 1, Status = "Applicant" },
            new TranslatorStatus { Id = 2, Status = "Certified" },
            new TranslatorStatus { Id = 3, Status = "Deleted" }
            );

            modelBuilder.Entity<TranslationJobStatus>().HasData(
           new TranslationJobStatus { Id = 1, Status = "New" },
           new TranslationJobStatus { Id = 2, Status = "InProgress" },
           new TranslationJobStatus { Id = 3, Status = "Completed" }
           );
        }
    }
}
