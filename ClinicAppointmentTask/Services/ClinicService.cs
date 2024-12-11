using ClinicAppointmentTask.Models;
using ClinicAppointmentTask.Repositories;

namespace ClinicAppointmentTask.Services
{
    public class ClinicService : IClinicService
    {
        private readonly IClinicRepository _clinicRepository;

        public ClinicService(IClinicRepository clinicRepository)
        {
            _clinicRepository = clinicRepository;

        }
        public List<Clinic> GetAllClinic()
        {
            try
            {
                // Retrieve all Clinics from the repository  order by Specialization
                var clinics = _clinicRepository.GetAll().OrderBy(c => c.Specialization).ToList();
                if (clinics == null || clinics.Count == 0)// Check if the Clinic list is null or empty 
                {
                    throw new InvalidOperationException("No clinics found.");
                }
                return clinics;
            }
            catch (InvalidOperationException)
            {

                throw;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error: {ex.Message}");


                throw;
            }
        }
        public string AddClinic(Clinic clinic)
        {
            try
            {
                if (clinic.Specialization == null)//Check If clinic is null
                {
                    throw new ArgumentException("Specialization  is required.");
                }
                if (clinic.NoOfSlots <=0)//Check If No Of Slots is lESS THAN ZERO
                {
                    throw new ArgumentException("No Of Slots must be greater than zero");
                }
                return _clinicRepository.Add(clinic);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error: {ex.Message}");


                throw;
            }
        }

    }
}
