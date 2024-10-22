using Microsoft.EntityFrameworkCore;

namespace PatientMangerServices.Models;

public class PmDbContext : DbContext
{
    public PmDbContext(DbContextOptions<PmDbContext> options) : base(options)
    {
    }

    public DbSet<Patient> Patients { get; set; }
}