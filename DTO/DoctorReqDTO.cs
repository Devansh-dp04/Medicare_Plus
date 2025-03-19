using System.ComponentModel.DataAnnotations;

using MedicarePlus.Models;

using AutoMapper;

namespace MedicarePlus.DTOs
{
    // Doctor-related DTOs
    public class DoctorPostRequestDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfJoining { get; set; }
        public DateTime? DateOfRelieving { get; set; }

        [Required]
        [StringLength(15)]
        public string MobileNo { get; set; }

        [StringLength(15)]
        public string EmergencyNo { get; set; }
        public int SpecializationId { get; set; }

        [Required]
        public string Qualification { get; set; }

        [Required]
        public string LicenseNumber { get; set; }
    }
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Doctor mappings
            CreateMap<DoctorPostRequestDto, User>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => System.DateTime.UtcNow))
                .ForMember(dest => dest.Password, opt => opt.Ignore())
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.RoleId, opt => opt.Ignore());

            CreateMap<DoctorPostRequestDto, Doctor>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => System.DateTime.UtcNow))
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.Specialization, opt => opt.Ignore())
                .ForMember(dest => dest.Appointments, opt => opt.Ignore());
            
        }
    }
}






