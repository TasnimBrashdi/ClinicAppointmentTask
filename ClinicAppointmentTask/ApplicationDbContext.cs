using ClinicAppointmentTask.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicAppointmentTask
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Clinic>()
                        .HasIndex(c => c.Specialization)
                        .IsUnique();

        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<Booking> Bookings { get; set; }
    }
}
