using best_slim_jim_engine.Models;

namespace best_slim_jim_engine.Repositories;

public interface IVoteRepository
{
    Task<Vote> CreateVoteAsync(int flavorId);
    Task<List<Vote>> GetVotesByFlavorIdAsync(int flavorId);
    Task<int> GetVotesCountForFlavorAsync(int flavorId);
}