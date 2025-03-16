using System.ComponentModel.DataAnnotations;

namespace MedicarePlus.Models
{
    public class Patient
    {
        [Key]
        public int PatientId { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Gender { get; set; }

        [StringLength(12)]
        public string AadharNo { get; set; }

        public string Address { get; set; }
        public string City { get; set; }

        [Required]
        [StringLength(15)]
        public string MobileNo { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public bool Active { get; set; } = true;

        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}
