using ClinicAppointmentTask.Models;
using ClinicAppointmentTask.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClinicAppointmentTask.controller
{
    [ApiController]
    [Route("api/[Controller]")]
    public class PatientCotroller : ControllerBase
    {
        private readonly IPatientService _patientService;
        public PatientCotroller(IPatientService patientService)
        {
            _patientService = patientService;
        }
        [HttpGet("GetAllPatient")]
        public IActionResult GetAllPatient()
        {
            try
            {
                var patients = _patientService.GetAllPatient();
                return Ok(patients);
            }
            catch (InvalidOperationException ex) // Specific business logic exceptions
            {
                return NotFound(ex.Message); // Return 404 with the message
            }
            catch (Exception)
            {
                // Log the exception using a logging framework 
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
        [HttpPost]
        public IActionResult Add(string name, int age, Patient.GENDER gen)
        {
            try
            {   // Call the service to add the patient
                string NewPatient = _patientService.AddPatient(new Patient
                {
                    Name = name,
                    Age = age,
                    gender = gen
                });
                return Created(string.Empty, NewPatient);
            }
            catch (ArgumentException ex) // Business logic errors 
            {
                return BadRequest(ex.Message); // Return 400 Bad Request
            }
            catch (Exception) 
            {
           
                // Return a generic error message to the client
                return StatusCode(500, "An unexpected error occurred while processing your request.");
            }
        }


    }
}
