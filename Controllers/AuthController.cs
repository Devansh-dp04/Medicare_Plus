
using MedicarePlus.DTOs;
using Microsoft.AspNetCore.Mvc;
using MedicarePlus.Services;
using MedicarePlus.DTO;

namespace MedicarePlus.Controllers;
[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;
    private readonly PassGenService _passGenService;

    public AuthController(IAuthService userService, ILogger<AuthController> logger, PassGenService passGenService)
    {
        _authService = userService;
        _logger = logger;
        _passGenService = passGenService;
    }

    [HttpPost("add-doctor")]
    public async Task<IActionResult> AddDoctor( DoctorPostRequestDto doctorPostRequestDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }        
        int createdBy = 1;
         return await _authService.AddDoctor(doctorPostRequestDto, createdBy);       
        
    }

    [HttpGet("sign-in")]

    public async Task<IActionResult> Sign_in_User(string Email, string password)
    {
        var signinresponse =  await _authService.SigninUser(Email, password);
        return Ok(signinresponse);

    }

    [HttpPut("Generate-new-password")]
    public async Task<IActionResult> Generate_New_Password(PasswordGenerationDTO passwordGeneration)
    {
        var response = await _passGenService.GenerateNewPassword(passwordGeneration);
        return Ok(response);
    }
}
