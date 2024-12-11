using ClinicAppointmentTask.Models;
using ClinicAppointmentTask.Repositories;

namespace ClinicAppointmentTask.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IClinicService _clinicService; //Note: U  must call service not repo  if repo is not for this service 
        public BookingService(IBookingRepository bookingRepository, IClinicService clinicService)
        {
            _bookingRepository = bookingRepository;
            _clinicService = clinicService;

        }

        public List<Booking> GetBookingByIdPatient(int id)
        {
            //Get List of booking by input  patient's ID
            var booking = _bookingRepository.GetByPatient(id).ToList();
            try
            {

                if (booking == null)
                {
                    throw new KeyNotFoundException("booking not found.");
                }
                return booking;
            }
            catch (InvalidOperationException)
            {
                // Re-throw "specific exceptions" 
                throw;
            }
            catch (Exception ex)
            {
                // Log the exception 
                Console.WriteLine($"Error: {ex.Message}");

                // Rethrow the "General exception" 
                throw;
            }
        }

        public List<Booking> GetBookingByIdClinic(int id)
        {      //Get List of booking by input  Clinic's ID
            var booking = _bookingRepository.GetByClinic(id).ToList();
            try
            {
                if (booking == null)
                {
                    throw new KeyNotFoundException("booking not found.");
                }
                return booking;
            }
            catch (InvalidOperationException)
            {
                // Re-throw "specific exceptions" 
                throw;
            }
            catch (Exception ex)
            {
                // Log the exception 
                Console.WriteLine($"Error: {ex.Message}");

                // Rethrow the "General exception" 
                throw;
            }
        }
        public List<Booking> GetByName(string name)
        {
            //Get List of booking by input  Patent's Name
            var booking = _bookingRepository.GetByName(name);
            try
            {
                if (booking == null)
                {
                    throw new KeyNotFoundException("booking not found.");
                }
                return booking;
            }
            catch (InvalidOperationException)
            {
                // Re-throw "specific exceptions" 
                throw;
            }
            catch (Exception ex)
            {
                // Log the exception 
                Console.WriteLine($"Error: {ex.Message}");

                // Rethrow the "General exception" 
                throw;
            }
        }

        public void AddBooking(Booking bookings, Clinic clinic)
        {
            try
            {
                //Check if the date in past 
                if (bookings.Date < DateTime.Now)
                {
                    throw new ArgumentException("Appointment date cannot be in the past.");
                }
                var existingBooking = _bookingRepository.GetByPatientAndClinic(
                   bookings.PatientID,
                   bookings.ClinicId,
                   bookings.Date

                    );

                //Check if patient are already has booking in selected date 
                if (existingBooking != null)
                {
                    throw new ArgumentException("The patient already has a booking in this clinic on the selected date.");
                }
                // Check if the clinic has available slots for the selected date
                int availableSlots = clinic.NoOfSlots;

                //Count  booking by date and id clinic 
                int BookingDaily = _bookingRepository.BookingCount(

                    bookings.ClinicId,
                    bookings.Date

                    );


                if (BookingDaily >= availableSlots)
                {
                    throw new InvalidOperationException("The clinic has the maximum number of appointments for the selected date.");
                }
                // Create and save the new booking
                var booking = new Booking
                {
                    PatientID = bookings.PatientID,
                    ClinicId = bookings.ClinicId,
                    Date = bookings.Date,
                    Slots_Number = BookingDaily + 1 // Increment slot number
                };


                _bookingRepository.Add(booking);
            }
            catch (ArgumentNullException ex)
            {
                //null arguments
                Console.WriteLine($"Error: {ex.Message}");
                throw new ArgumentException("Invalid input data.", ex);
            }
            catch (ArgumentException ex)
            {
                //argument errors
                Console.WriteLine($"Error: {ex.Message}");
                throw; // Rethrow to maintain stack trace
            }
            catch (InvalidOperationException ex)
            {
                //business rule
                Console.WriteLine($"Error: {ex.Message}");
                throw; // Rethrow to maintain stack trace
            }
            catch (Exception ex)
            {
                //unexpected errors
                Console.WriteLine($"Error: {ex.Message}"); ;
                throw new ApplicationException("An unexpected error occurred while booking the appointment. Please try again later.", ex);
            }
        }
    }
}
