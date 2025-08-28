using best_slim_jim_engine.Models;
using best_slim_jim_engine.Models.DTOs;

namespace best_slim_jim_engine.Repositories;

public interface ISlimJimFlavorRepository
{
    Task<List<SlimJimFlavor>> GetAllFlavorsAsync();
    Task<SlimJimFlavor?> GetFlavorByIdAsync(int flavorId);
    Task<List<FlavorVoteSummaryDto>> GetFlavorsWithVoteCountsAsync();
}