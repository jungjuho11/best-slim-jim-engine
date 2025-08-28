using best_slim_jim_engine.Models;
using best_slim_jim_engine.Models.DTOs;
using Supabase;

namespace best_slim_jim_engine.Repositories;

public class SlimJimFlavorRepository : ISlimJimFlavorRepository
{
    private readonly Supabase.Client _supabaseClient;

    public SlimJimFlavorRepository(Supabase.Client supabaseClient)
    {
        _supabaseClient = supabaseClient;
    }

    public async Task<List<SlimJimFlavor>> GetAllFlavorsAsync()
    {
        var response = await _supabaseClient
            .From<SlimJimFlavor>()
            .Get();
        
        return response.Models;
    }

    // public async Task TestTableAccessAsync()
    // {
    //     try
    //     {
    //         Console.WriteLine("=== Testing Table Access ===");
    //
    //         // Try to access the table with different names
    //         var tableNames = new[] { "flavors", "public.flavors", "Flavors", "FLAVORS" };
    //
    //         foreach (var tableName in tableNames)
    //         {
    //             try
    //             {
    //                 Console.WriteLine($"Testing table name: {tableName}");
    //
    //                 // Use reflection to set the table name temporarily
    //                 var response = await _supabaseClient
    //                     .From<SlimJimFlavor>()
    //                     .Get();
    //
    //                 Console.WriteLine($"  Result: {response.Models.Count} models");
    //             }
    //             catch (Exception ex)
    //             {
    //                 Console.WriteLine($"  Error: {ex.Message}");
    //             }
    //         }
    //
    //     }
    //     catch (Exception ex)
    //     {
    //         Console.WriteLine($"❌ Error in table access test: {ex.Message}");
    //     }
    // }

    // public async Task<List<SlimJimFlavor>> GetAllFlavorsAsync()
    // {
    //     try
    //     {
    //         Console.WriteLine("=== GetAllFlavorsAsync Debug ===");
    //         Console.WriteLine("Starting Supabase query...");
    //
    //         // Test table access first
    //         await TestTableAccessAsync();
    //
    //         var response = await _supabaseClient
    //             .From<SlimJimFlavor>()
    //             .Get();
    //
    //         Console.WriteLine($"Query completed. Response received.");
    //         Console.WriteLine($"Models count: {response.Models.Count}");
    //         Console.WriteLine($"Response status: {response.ResponseMessage?.StatusCode}");
    //
    //         if (response.Models.Count > 0)
    //         {
    //             Console.WriteLine("First few flavors:");
    //             foreach (var flavor in response.Models.Take(3))
    //             {
    //                 Console.WriteLine($"  ID: {flavor.Id}, Name: {flavor.Flavor}, Description: {flavor.Description}");
    //             }
    //         }
    //         else
    //         {
    //             Console.WriteLine("⚠️ No models returned from Supabase!");
    //         }
    //
    //         return response.Models;
    //     }
    //     catch (Exception ex)
    //     {
    //         Console.WriteLine($"❌ Error in GetAllFlavorsAsync: {ex.Message}");
    //         throw;
    //     }
    // }


public async Task<SlimJimFlavor?> GetFlavorByIdAsync(int id)
    {
        var response = await _supabaseClient
            .From<SlimJimFlavor>()
            .Where(x => x.Id == id)
            .Get();
        
        return response.Models.FirstOrDefault();
    }

    public async Task<List<FlavorVoteSummaryDto>> GetFlavorsWithVoteCountsAsync()
    {
        // This is more complex - we'll need to join flavors with votes
        // For now, let's return a basic implementation
        var flavors = await GetAllFlavorsAsync();
        var result = new List<FlavorVoteSummaryDto>();
        
        foreach (var flavor in flavors)
        {
            // We'll implement vote counting in the next step
            result.Add(new FlavorVoteSummaryDto
            {
                FlavorId = flavor.Id,
                FlavorName = flavor.Flavor,
                VoteCount = 0, // Placeholder for now
            });
        }
        
        return result;
    }
}

