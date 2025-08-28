using best_slim_jim_engine.Models;
using best_slim_jim_engine.Models.DTOs;
using best_slim_jim_engine.Repositories;

namespace best_slim_jim_engine.Services;

public class SlimJimFlavorService : ISlimJimFlavorService
{
    private readonly ISlimJimFlavorRepository _flavorRepository;
    private readonly IVoteRepository _voteRepository;
    
    // Constructor
    public SlimJimFlavorService(
        ISlimJimFlavorRepository flavorRepository,
        IVoteRepository voteRepository)
    {
        _flavorRepository = flavorRepository;
        _voteRepository = voteRepository;
    }
    
    public async Task<List<FlavorVoteSummaryDto>> GetFlavorsWithVoteCountsAsync()
    {
        // Get all flavors
        var flavors = await _flavorRepository.GetAllFlavorsAsync();
        var result = new List<FlavorVoteSummaryDto>();

        // Process each flavor to get vote counts
        foreach (var flavor in flavors)
        {
            var voteCount = await _voteRepository.GetVotesCountForFlavorAsync(flavor.Id);
            var votes = await _voteRepository.GetVotesByFlavorIdAsync(flavor.Id);
            
            result.Add(new FlavorVoteSummaryDto
            {
                FlavorId = flavor.Id,
                FlavorName = flavor.Flavor,
                VoteCount = voteCount,
            });
        }

        return result;
    }

    public async Task<SlimJimFlavor?> GetFlavorByIdAsync(int id)
    {
        return await _flavorRepository.GetFlavorByIdAsync(id);
    }
}