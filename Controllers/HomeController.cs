using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Data;

[ApiController]
[Route("[controller]")]
public class HomeController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public HomeController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet("now")]
    public IActionResult GetCurrentTime()
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        Console.WriteLine(connectionString);

        try
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();

            using var command = new NpgsqlCommand("SELECT NOW();", connection);
            var result = command.ExecuteScalar();
            Console.WriteLine(result);

            return Ok(new { CurrentTime = result });
        }
        catch (Exception ex)
        {
            Console.WriteLine("error");
            return StatusCode(500, new { Error = ex.Message });
        }
    }

    //[HttpGet("now")]
    //public IActionResult GetCurrentTime()
    //{
    //    try
    //    {
    //        var currentTime = DateTime.UtcNow;
    //        Console.WriteLine(currentTime);
    //        return Ok(new { CurrentTime = currentTime });
    //    }
    //    catch (Exception ex)
    //    {
    //        Console.WriteLine("error in current time without db");
    //        return StatusCode(500, new { Error = ex.Message });
    //    }
    //}
}
