using ClinicAppointmentTask.Models;

namespace ClinicAppointmentTask.Repositories
{
    public interface IClinicRepository
    {
        string Add(Clinic clinic);
        IEnumerable<Clinic> GetAll();
    }
}