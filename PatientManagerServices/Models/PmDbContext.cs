using Microsoft.EntityFrameworkCore;

namespace PatientManagerServices.Models;

public class PmDbContext : DbContext
{
    public PmDbContext(DbContextOptions<PmDbContext> options) : base(options)
    {
    }

    public DbSet<Patient> Patients { get; set; }
    public DbSet<Illness> Illnesses { get; set; }
    public DbSet<Examination> Examinations { get; set; }
    public DbSet<MedicalHistory> MedicalHistories { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Patient>().HasAlternateKey(u => u.NameNormalized);
    }    
    public DbSet<Prescription> Prescriptions { get; set; }
}