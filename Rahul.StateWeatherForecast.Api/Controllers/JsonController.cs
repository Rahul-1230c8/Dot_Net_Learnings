using Microsoft.AspNetCore.Mvc;
using Rahul.StateWeatherForecast.Api.Models;
using Rahul.StateWeatherForecast.Api.Data;
using System.Text.Json;

namespace Rahul.StateWeatherForecast.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JsonController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<JsonController> _logger;

        public JsonController(AppDbContext context, ILogger<JsonController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost]
        [Consumes("application/json")]
        public async Task<IActionResult> SaveJson1([FromBody] JsonElement jsonData)
        {
            if (jsonData.ToString() =="{}")
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            var rawJson = new RawJson
            {
                ObjName = "StateWeather",
                JsonData = jsonData.ToString()
            };
            try
            {
                _context.RawJsons.Add(rawJson);
                await _context.SaveChangesAsync();
                return Ok(new { message = "JSON saved successfully" });
            }
            catch (Exception ex)
            {
                // Log the full exception for diagnostics
                _logger.LogError(ex, "Error saving JSON to database");

                // Return minimal diagnostic information to the caller for debugging.
                // In production avoid returning full exception details.
                return Problem(detail: ex.Message, title: "Database error");
            }
        }
    }
}