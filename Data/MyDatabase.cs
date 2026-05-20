using Lab_Results.Config;
using Lab_Results.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Lab_Results.Data
{
    public class MyDatabase(DbContextOptions options)
        : IdentityDbContext<User>(options)
    {

        public DbSet<Patient> Patients => Set<Patient>();
        public DbSet<Result> Results => Set<Result>();

        public DbSet<AccessLink> Links => Set<AccessLink>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PatientConfig).Assembly);

            modelBuilder.Entity<Result>()
                .Property(r => r.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");


            modelBuilder.Entity<AccessLink>()
                   .HasIndex(x => x.Code)
                .IsUnique();

            modelBuilder.Entity<AccessLink>()
                .HasIndex(x => x.Sid)
                .IsUnique();





        }
    }
}