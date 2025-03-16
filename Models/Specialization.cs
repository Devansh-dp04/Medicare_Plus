using System.ComponentModel.DataAnnotations;

namespace MedicarePlus.Models
{
    public class Specialization
    {
        [Key]
        public int SpecializationId { get; set; }

        [Required]
        [StringLength(100)]
        public string SpecializationName { get; set; }

        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public virtual ICollection<Doctor> Doctors { get; set; }

    }
}
