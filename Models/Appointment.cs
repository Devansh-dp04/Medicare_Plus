using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MedicarePlus.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }

        [ForeignKey("Patient")]
        public int PatientId { get; set; }

        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }

        public DateTime AppointmentStarts { get; set; }
        public DateTime AppointmentEnds { get; set; }

        [Required]
        public string Status { get; set; }

        public string AppointmentDescription { get; set; }

        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
        public virtual ICollection<PatientNote> PatientNotes { get; set; }
    }
}
