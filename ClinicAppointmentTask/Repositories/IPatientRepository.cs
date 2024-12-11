using ClinicAppointmentTask.Models;

namespace ClinicAppointmentTask.Repositories
{
    public interface IPatientRepository
    {
        string Add(Patient patient);
        IEnumerable<Patient> GetAll();
    }
}