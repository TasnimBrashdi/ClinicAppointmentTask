using ClinicAppointmentTask.Models;

namespace ClinicAppointmentTask.Services
{
    public interface IPatientService
    {
        string AddPatient(Patient patient);
        List<Patient> GetAllPatient();
    }
}