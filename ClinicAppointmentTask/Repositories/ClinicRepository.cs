using ClinicAppointmentTask.Models;

namespace ClinicAppointmentTask.Repositories
{
    public class ClinicRepository : IClinicRepository
    {
        private readonly ApplicationDbContext _context;

        public ClinicRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Clinic> GetAll()
        {
            return _context.Clinics.ToList();
        }

        public string Add(Clinic clinic)
        {
            _context.Clinics.Add(clinic);
            _context.SaveChanges();

            return clinic.Specialization;
        }
    }
}
