using ClinicAppointmentTask.Models;
using ClinicAppointmentTask.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClinicAppointmentTask.controller
{
    [ApiController]
    [Route("api/[Controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }
        [HttpGet("GetBookingByPatient/{pid}")]
        public IActionResult GetBookingByIdPatient(int pid)
        {
            try
            {
                var booking = _bookingService.GetBookingByIdPatient(pid);
                return Ok(booking);
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

        [HttpGet("GetBookingByIdClinic/{cid}")]
        public IActionResult GetBookingByIdClinic(int cid)
        {
            try
            {
                var booking = _bookingService.GetBookingByIdClinic(cid);
                return Ok(booking);
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
        [HttpGet("GetBookingByPatientName/{name}")]
        public IActionResult GetByName(string name)
        {
            try
            {
                var booking = _bookingService.GetByName(name);
                return Ok(booking);
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
        public IActionResult AddBooking(int cid, int pid, DateTime dateTime)
        {
            try
            {
                var booking = new Booking
                {
                    ClinicId = cid,
                    PatientID = pid,
                    Date = dateTime
                };

                var clinic = new Clinic
                {
                    CID = cid
                };

                _bookingService.AddBooking(booking, clinic);
                return Created();
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
