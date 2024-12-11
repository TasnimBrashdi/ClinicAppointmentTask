using ClinicAppointmentTask.Models;
using ClinicAppointmentTask.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClinicAppointmentTask.controller
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ClinicController : ControllerBase
    {
        private readonly IClinicService _clinicService;
        public ClinicController(IClinicService clinicService)
        {
            _clinicService = clinicService;
        }
        [HttpGet("GetAllClinic")]
        public IActionResult GetAllClinic()
        {
            try
            {
                var clinics = _clinicService.GetAllClinic();
                return Ok(clinics);
            }
            catch (InvalidOperationException ex) // Specific business logic exceptions
            { 
                // Return 404 with the message
                return NotFound(ex.Message); 
              
            }
            catch (Exception)
            {
                // Log the exception using a logging framework 
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
        [HttpPost]
        public IActionResult Add(string Specialization, int No)
        {
            try
            {
                //Add New clinic
                string NewClinic = _clinicService.AddClinic(new Clinic
                { // Set the clinic specialization
                    Specialization = Specialization,
                    NoOfSlots = No  // Set the number of slots

                });
                // Return 201 Created with the new clinic details
                return Created(string.Empty, NewClinic);
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
