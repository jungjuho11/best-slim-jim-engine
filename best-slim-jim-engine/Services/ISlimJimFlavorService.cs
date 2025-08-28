using best_slim_jim_engine.Models;
using best_slim_jim_engine.Models.DTOs;

namespace best_slim_jim_engine.Services;

public interface ISlimJimFlavorService
{
    Task<List<FlavorVoteSummaryDto>> GetFlavorsWithVoteCountsAsync();
    Task<SlimJimFlavor?> GetFlavorByIdAsync(int id);
}