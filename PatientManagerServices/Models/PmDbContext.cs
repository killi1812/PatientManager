using Microsoft.EntityFrameworkCore;

namespace PatientManagerServices.Models;

public class PmDbContext : DbContext
{
    public PmDbContext(DbContextOptions<PmDbContext> options) : base(options)
    {
    }

    public DbSet<Patient> Patients { get; set; }
}