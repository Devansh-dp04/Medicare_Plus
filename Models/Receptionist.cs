using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MedicarePlus.Models
{
    public class Receptionist
    {
        [Key]
        public int ReceptionistId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        public string Qualification { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public User User { get; set; }
    }
}
