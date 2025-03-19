using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MedicarePlus.Data;
using MedicarePlus.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MedicarePlus.Services
{
    public class PassGenService
    {
        ApplicationDbContext _context;

        public PassGenService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> GenerateNewPassword(PasswordGenerationDTO passwordGeneration)
        {
            if (passwordGeneration.NewPassword != passwordGeneration.ConfirmPassword)
            {
                return new ObjectResult(new
                {
                    Success = false,
                    Message = "New Password and Confirm Password don't match"
                })
                {
                    StatusCode = StatusCodes.Status406NotAcceptable
                };

            }
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == passwordGeneration.Email);
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
            if (user.Password != HashPassword(passwordGeneration.Password))
            {
                return new ObjectResult(new
                {
                    Success = false,
                    Message = "Invalid system generated password"
                })
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
            }
            
            string newPassword = HashPassword(passwordGeneration.NewPassword);
            user.Password = newPassword;
            await _context.SaveChangesAsync();
            return new ObjectResult(new
            {
                Success = true,
                Message = "Password updated successfully"
            })
            {
                StatusCode = StatusCodes.Status200OK
            };
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
