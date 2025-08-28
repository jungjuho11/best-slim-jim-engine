using best_slim_jim_engine.Models.DTOs;
using best_slim_jim_engine.Repositories;

namespace best_slim_jim_engine.Services;

public class VoteService : IVoteService
{
    private readonly IVoteRepository _voteRepository;
    private readonly ISlimJimFlavorRepository _flavorRepository;

    public VoteService(
        IVoteRepository voteRepository,
        ISlimJimFlavorRepository flavorRepository)
    {
        _voteRepository = voteRepository;
        _flavorRepository = flavorRepository;
    }

    public async Task<bool> CreateVoteAsync(int flavorId)
    {
        try
        {
            // Check if flavor exists
            var flavor = await _flavorRepository.GetFlavorByIdAsync(flavorId);
            if (flavor == null)
            {
                return false; // Flavor doesn't exist
            }

            // Create the vote
            await _voteRepository.CreateVoteAsync(flavorId);
            return true; // Vote created successfully
        }
        catch
        {
            return false; // Something went wrong
        }
    }

    public async Task<List<FlavorVoteSummaryDto>> GetVoteResultsAsync()
    {
        // This method does the same as GetFlavorsWithVoteCountsAsync
        // We could refactor to avoid duplication later
        var flavors = await _flavorRepository.GetAllFlavorsAsync();
        var result = new List<FlavorVoteSummaryDto>();

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
}