using Microsoft.AspNetCore.Mvc;
using best_slim_jim_engine.Models.DTOs;
using best_slim_jim_engine.Services;

namespace best_slim_jim_engine.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SlimJimFlavorController : ControllerBase
{
    private readonly ISlimJimFlavorService _flavorService;

    public SlimJimFlavorController(ISlimJimFlavorService flavorService)
    {
        _flavorService = flavorService;
    }

    [HttpGet]
    public async Task<ActionResult<List<FlavorVoteSummaryDto>>> GetFlavorsWithVotes()
    {
        try
        {
            var result = await _flavorService.GetFlavorsWithVoteCountsAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while retrieving flavors", error = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<FlavorVoteSummaryDto>> GetFlavorById(int id)
    {
        try
        {
            var flavor = await _flavorService.GetFlavorByIdAsync(id);
            if (flavor == null)
            {
                return NotFound(new { message = $"Flavor with ID {id} not found" });
            }

            // Convert to DTO format
            var result = new FlavorVoteSummaryDto
            {
                FlavorId = flavor.Id,
                FlavorName = flavor.Flavor
            };

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while retrieving the flavor", error = ex.Message });
        }
    }
}