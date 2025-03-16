using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MedicarePlus.Models
{
    public class PatientNote
    {
        [Key]
        public int PatientNoteId { get; set; }

        [ForeignKey("Appointment")]
        public int AppointmentId { get; set; }

        [Required]
        public string NoteText { get; set; }

        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Appointment Appointment { get; set; }
    }
}
