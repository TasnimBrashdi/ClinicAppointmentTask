using ClinicAppointmentTask.Models;

namespace ClinicAppointmentTask.Services
{
    public interface IClinicService
    {
        string AddClinic(Clinic clinic);
        List<Clinic> GetAllClinic();
    }
}