using Microsoft.AspNetCore.Mvc;
using best_slim_jim_engine.Models.DTOs;
using best_slim_jim_engine.Services;

namespace best_slim_jim_engine.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VoteController : ControllerBase
{
    private readonly IVoteService _voteService;

    public VoteController(IVoteService voteService)
    {
        _voteService = voteService;
    }

    [HttpPost]
    public async Task<ActionResult> CreateVote([FromBody] CreateVoteDto voteDto)
    {
        try
        {
            var success = await _voteService.CreateVoteAsync(voteDto.FlavorId);
            
            if (success)
            {
                return Ok(new { message = "Vote recorded successfully" });
            }
            else
            {
                return BadRequest(new { message = "Invalid flavor ID or vote could not be recorded" });
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while recording the vote", error = ex.Message });
        }
    }

    [HttpGet("results")]
    public async Task<ActionResult<List<FlavorVoteSummaryDto>>> GetVoteResults()
    {
        try
        {
            var result = await _voteService.GetVoteResultsAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while retrieving vote results", error = ex.Message });
        }
    }
}