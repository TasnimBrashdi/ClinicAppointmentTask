using ClinicAppointmentTask.Models;
using ClinicAppointmentTask.Repositories;

namespace ClinicAppointmentTask.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;

        public PatientService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;

        }

        public List<Patient> GetAllPatient()
        {
            try
            {
                // Retrieve all patients from the repository  order by name
                var patient = _patientRepository.GetAll().OrderBy(b => b.Name).ToList();
                if (patient == null || patient.Count == 0)  // Check if the patient list is null or empty 
                {
                    throw new InvalidOperationException("No patients found.");
                }
                return patient;
            }
            catch (InvalidOperationException)
            {
                // Re-throw "specific exceptions" ,because it’s a logical exception related to business logic and should propagate.
                throw;
            }
            catch (Exception ex)
            {
                // Log the exception 
                Console.WriteLine($"Error: {ex.Message}");

                // Rethrow the "General exception" to ensure it's not silently suppressed
                throw;
            }
        }
        public string AddPatient(Patient patient)
        {
            try
            {
                if (patient.Name == null)
                {
                    throw new ArgumentException("Patient name is required.");
                }
                if (patient.Age<=0)
                {
                    throw new ArgumentException("AGE Must be greater than zero.");
                }
                // Return list of patients
                return _patientRepository.Add(patient);
            }
            catch (InvalidOperationException)
            {
                // Re-throw "specific exceptions" ,because it’s a logical exception related to business logic and should propagate.
                throw;
            }
            catch (Exception ex)
            {
                // Log the exception 
                Console.WriteLine($"Error: {ex.Message}");

                // Rethrow the "General exception" to ensure it's not silently suppressed
                throw;
            }
        }
    }
}
