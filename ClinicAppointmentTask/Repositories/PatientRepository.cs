using ClinicAppointmentTask.Models;

namespace ClinicAppointmentTask.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly ApplicationDbContext _context;

        public PatientRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Patient> GetAll()
        {
            return _context.Patients.ToList();
        }

        public string Add(Patient patient)
        {
            _context.Patients.Add(patient);
            _context.SaveChanges();

            return patient.Name;
        }
    }
}
