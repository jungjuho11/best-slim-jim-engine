using best_slim_jim_engine.Models.DTOs;

namespace best_slim_jim_engine.Services;

public interface IVoteService
{
    Task<bool> CreateVoteAsync(int flavorId);
    Task<List<FlavorVoteSummaryDto>> GetVoteResultsAsync();
}