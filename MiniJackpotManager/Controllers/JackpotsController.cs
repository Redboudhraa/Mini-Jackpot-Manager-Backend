using Microsoft.AspNetCore.Mvc;
using MiniJackpotManager.Models;
using MiniJackpotManager.Services;

namespace MiniJackpotManager.Controllers;

/// <summary>
/// Controller for managing jackpots
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class JackpotsController : ControllerBase
{
    private readonly IJackpotService _jackpotService;
    private readonly ILogger<JackpotsController> _logger;

    /// <summary>
    /// Constructor for JackpotsController
    /// </summary>
    /// <param name="jackpotService">Service for jackpot operations</param>
    /// <param name="logger">Logger for the controller</param>
    public JackpotsController(IJackpotService jackpotService, ILogger<JackpotsController> logger)
    {
        _jackpotService = jackpotService;
        _logger = logger;
    }

    /// <summary>
    /// Gets all jackpots
    /// </summary>
    /// <returns>A list of all jackpots</returns>
    [HttpGet]
    public async Task<IActionResult> GetAllJackpots()
    {
        try
        {
            var jackpots = await _jackpotService.GetAllJackpotsAsync();
            return Ok(jackpots);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all jackpots");
            return StatusCode(500, "An error occurred while retrieving jackpots");
        }
    }

    /// <summary>
    /// Contributes a specified amount to a jackpot
    /// </summary>
    /// <param name="id">ID of the jackpot</param>
    /// <param name="contribution">Contribution details including amount</param>
    /// <returns>Details of the contribution</returns>
    [HttpPost("{id}/contribute")]
    public async Task<IActionResult> ContributeToJackpot(int id, [FromBody] ContributionRequestDto contribution)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var contributionResponse = await _jackpotService.ContributeToJackpotAsync(id, contribution.Amount);
            return Ok(contributionResponse);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error contributing to jackpot with ID {id}");
            return StatusCode(500, "An error occurred while processing your contribution");
        }
    }
}
