using Lab_Results.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Lab_Results.Config
{
    public class PatientConfig :IEntityTypeConfiguration<Patient> 
    {

        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder
             .HasMany(p => p.Results)
               .WithOne(r => r.Patient)
                 .HasForeignKey(r => r.PatientId)
                    .OnDelete(DeleteBehavior.NoAction);

            builder.Property(x => x.Gender).HasConversion(o => o.ToString(),
             o => (Gender)Enum.Parse(typeof(Gender), o));
            builder
                  .HasIndex(p => p.Sid)
                    .IsUnique();
      



        }
    }
}
