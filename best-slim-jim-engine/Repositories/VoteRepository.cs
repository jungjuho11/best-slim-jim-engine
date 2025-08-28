using best_slim_jim_engine.Models;
using Supabase;

namespace best_slim_jim_engine.Repositories;

public class VoteRepository : IVoteRepository
{
    private readonly Supabase.Client _supabaseClient;

    public VoteRepository(Supabase.Client supabaseClient)
    {
        _supabaseClient = supabaseClient;
    }

    public async Task<Vote> CreateVoteAsync(int flavorId)
    {
        var vote = new Vote
        {
            FlavorId = flavorId,
            CreatedAt = DateTime.UtcNow
        };

        var response = await _supabaseClient
            .From<Vote>()
            .Insert(vote);

        return response.Models.First();
    }

    public async Task<List<Vote>> GetVotesByFlavorIdAsync(int flavorId)
    {
        var response = await _supabaseClient
            .From<Vote>()
            .Where(x => x.FlavorId == flavorId)
            .Get();

        return response.Models;
    }

    public async Task<int> GetVotesCountForFlavorAsync(int flavorId)
    {
        var votes = await GetVotesByFlavorIdAsync(flavorId);
        return votes.Count;
    }
}

