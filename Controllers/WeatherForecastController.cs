using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
namespace MedicarePlus.Controllers;
[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    [HttpGet("GetWeatherForecast")]
    public IActionResult Get(string Name)
    {
        return Ok("Perfect");
        
    }
}
