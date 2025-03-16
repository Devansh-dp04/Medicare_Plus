using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MedicarePlus.Models
{
    public class Doctor
    {
        [Key]
        public int DoctorId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [ForeignKey("Specialization")]
        public int SpecializationId { get; set; }

        [Required]
        public string Qualification { get; set; }

        [Required]
        public string LicenseNumber { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public User User { get; set; }
        public Specialization Specialization { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }

    }
}
