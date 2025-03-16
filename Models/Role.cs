using System.ComponentModel.DataAnnotations;

namespace MedicarePlus.Models
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [Required]
        [StringLength(100)]
        public string RoleName { get; set; }
        public virtual ICollection<User> Users { get; set; }        
    }
}
