using ClinicAppointmentTask.Models;

namespace ClinicAppointmentTask.Repositories
{
    public interface IBookingRepository
    {
        void Add(Booking booking);
        int BookingCount(int clinicId, DateTime date);
        IEnumerable<Booking> GetByClinic(int cid);
        List<Booking> GetByName(string name);
        IEnumerable<Booking> GetByPatient(int pid);
        Booking GetByPatientAndClinic(int patientId, int clinicId, DateTime date);
    }
}