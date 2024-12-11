using ClinicAppointmentTask.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicAppointmentTask.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ApplicationDbContext _context;

        public BookingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //Get Booking By input Id clinic
        public IEnumerable<Booking> GetByClinic(int cid)
        {
            return _context.Bookings.Where(p => p.ClinicId == cid).ToList();
        }

        //Get Booking By input Id Patient 
        public IEnumerable<Booking> GetByPatient(int pid)
        {
            return _context.Bookings.Where(p => p.PatientID == pid).ToList();
        }


        public void Add(Booking booking)
        {
            _context.Bookings.Add(booking);
            _context.SaveChanges();
        }

        //Get Booking By Input Name Patient including related Patient and Clinic data
        public List<Booking> GetByName(string name)
        {
            return _context.Bookings.Include(b => b.Patient)
           .Include(b => b.Clinic)
           .Where(b => b.Patient.Name.Contains(name))
           .ToList();
        }
        //Get Booking By specified Input Id patient,ID clinic AND DATE booking 
        public Booking GetByPatientAndClinic(int patientId, int clinicId, DateTime date)
        {
            return _context.Bookings.FirstOrDefault(b => b.PatientID == patientId && b.ClinicId == clinicId && b.Date.Date == date.Date);
        }
        //Count Booking By clinic ID and Date
        public int BookingCount(int clinicId, DateTime date)
        {
            return _context.Bookings.Count(b => b.ClinicId == clinicId && b.Date.Date == date.Date);
        }
    }
}
