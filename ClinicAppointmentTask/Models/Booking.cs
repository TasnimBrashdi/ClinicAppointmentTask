using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace ClinicAppointmentTask.Models
{
    [PrimaryKey(nameof(PatientID), nameof(ClinicId))]
    public class Booking
    {
        [ForeignKey("PId")]
        public int PatientID { get; set; }
        [JsonIgnore]
        public virtual Patient Patient { get; set; }
        [ForeignKey("CId")]
        public int ClinicId { get; set; }
        [JsonIgnore]
        public virtual Clinic Clinic { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int Slots_Number { get; set; } = 0;
    }
}
