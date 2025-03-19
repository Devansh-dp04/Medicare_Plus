using System.ComponentModel.DataAnnotations;
using AutoMapper;
using MedicarePlus.DTOs;
using MedicarePlus.Models;

namespace MedicarePlus.DTO
{
    public class ReceptionistRequestDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
       
        public DateTime DateOfJoining { get; set; }
        
        public DateTime? DateOfRelieving { get; set; }
        [Required]
        [StringLength(15)]
        public string MobileNo { get; set; }
        public string EmergencyNo { get; set; }
        [Required]
        public string Qualification { get; set; }
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            
            CreateMap<ReceptionistRequestDto, User>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))   
            .ForMember(dest => dest.Password, opt => opt.Ignore())                           
            .ForMember(dest => dest.Active, opt => opt.MapFrom(src => true))                 
            .ForMember(dest => dest.RoleId, opt => opt.Ignore());                            

            CreateMap<ReceptionistRequestDto, Receptionist>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))   
                .ForMember(dest => dest.UserId, opt => opt.Ignore())                             
                .ForMember(dest => dest.User, opt => opt.Ignore());

        }
    }
}
