using AutoMapper;
using MedicarePlus.Data;
using MedicarePlus.DTOs;
using MedicarePlus.Models;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Data;
using MedicarePlus.DTO;



namespace MedicarePlus.Services
{
    public interface IAuthService
    {
        Task<IActionResult> AddDoctor(DoctorPostRequestDto doctorPostRequestDto, int createdBy);
        Task<IActionResult> SigninUser(string email, string passwrod);

    }
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthService> _logger;
        private readonly JWTService jWTService;

        public AuthService(ApplicationDbContext context, IMapper mapper, ILogger<AuthService> logger, JWTService _jWTService)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            jWTService = _jWTService;
           
        }

        public async Task<IActionResult> SigninUser(string email, string password)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
                if (user == null)
                {
                    return new ObjectResult(new
                    {
                        Success = false,
                        Message = "User not found"
                    })
                    {
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }
                if (user.Password != HashPassword(password))
                {
                    return new ObjectResult(new
                    {
                        Success = false,
                        Message = "Invalid password"
                    })
                    {
                        StatusCode = StatusCodes.Status401Unauthorized
                    };
                }
                
                var Jwttoken = RoleBasedTokenGeneration(user.RoleId, user.Email, user.UserId);
                return new ObjectResult(new
                {
                    Token = Jwttoken,
                    Expiration = DateTime.UtcNow.AddHours(1)
                });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error signing in user");
                return new ObjectResult(new
                {
                    Success = false,
                    Message = "Unable to sign in user"
                })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
        public async Task<IActionResult> AddDoctor(DoctorPostRequestDto doctorPostRequestDto, int createdBy)
        {
            try
            {
                // Check if email already exists
                if (await _context.Users.AnyAsync(u => u.Email == doctorPostRequestDto.Email))
                {
                    return new ObjectResult(new
                    {
                        Success = false,
                        Message = "Email already registered"
                    })
                    {
                        StatusCode = StatusCodes.Status409Conflict 
                    };

                }

                var specialization = await _context.Specializations.FindAsync(doctorPostRequestDto.SpecializationId);
                if (specialization == null)
                {
                    return new ObjectResult(new
                    {
                        Success = false,
                        Message = "Specialization does not exist"
                    })
                    {
                        StatusCode = StatusCodes.Status404NotFound 
                    };
                }

                // Get doctor role ID
                var doctorRole = await _context.Roles.FirstOrDefaultAsync(r => r.RoleName == "doctor");                

                // Generate random password
                string password = GenerateRandomPassword();
                string hashedPassword = HashPassword(password);

                // Map DTO to User entity
                var user = _mapper.Map<User>(doctorPostRequestDto);
                user.RoleId = doctorRole.RoleId;
                user.Password = hashedPassword;
                user.CreatedBy = createdBy;

                // Map DTO to Doctor entity
                var doctor = _mapper.Map<Doctor>(doctorPostRequestDto);

                // Create relationship
                doctor.User = user;

                // Add to context
                await _context.Doctors.AddAsync(doctor);
                await _context.SaveChangesAsync();
                

                // Log the password for development purposes
                _logger.LogInformation($"Generated password for doctor {user.Email}: {password}");
                Console.WriteLine($"Generated password for doctor {user.Email}: {password}");

                return new ObjectResult(doctor)
                {
                    StatusCode = StatusCodes.Status200OK,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding doctor");
                return new ObjectResult(new
                {
                    Success = false,
                    Message = "Unable to Add doctor"
                })
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                };
            }
        }
        public async Task<IActionResult> AddReceptionist(ReceptionistRequestDto receptionistRequest, int createdBy)
        {
            
        }
        private string RoleBasedTokenGeneration(int roleid, string emailid, int userId)
        {
            if (roleid == 1)
            {   
                string role = "Admin";
                var jwttoken = jWTService.GenerateToken(role, emailid, userId);
                return jwttoken;
            }
            else if (roleid == 2)
            {
                string role = "Doctor";
                var jwttoken = jWTService.GenerateToken(role, emailid, userId);
                return jwttoken;
            }
            else if (roleid == 3)
            {
                string role = "receptionist";
                var jwttoken = jWTService.GenerateToken(role, emailid, userId);
                return jwttoken;
            }
            else 
            {
                string role = "Patient";
                var jwttoken = jWTService.GenerateToken(role, emailid, userId);
                return jwttoken;
            }
            

        }
        private string GenerateRandomPassword()
        {
            // Generate 8-digit random alphanumeric password
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 8)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}
