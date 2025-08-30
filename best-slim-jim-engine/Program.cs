using best_slim_jim_engine.Repositories;
using best_slim_jim_engine.Services;

namespace best_slim_jim_engine;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        if (builder.Environment.IsDevelopment())
        {
            builder.Configuration.AddUserSecrets<Program>();
        }

        // Add services to the container.
        // dont need this since no authentication currently
        // builder.Services.AddAuthorization();

        // Add Supabase client configuration
        builder.Services.AddSingleton<Supabase.Client>(provider =>
        {
            // this gives us read access from appsettings.json
            var configuration = provider.GetRequiredService<IConfiguration>();
            var supabaseUrl = configuration["Supabase:Url"];
            var supabaseAnonKey = configuration["Supabase:AnonKey"];

            if (string.IsNullOrEmpty(supabaseUrl) || string.IsNullOrEmpty(supabaseAnonKey))
            {
                throw new InvalidOperationException(
                    "Supabase configuration is missing. Please check your appsettings.json file!");
            }

            return new Supabase.Client(supabaseUrl, supabaseAnonKey);
        });
        
        // Register Repositories
        builder.Services.AddScoped<ISlimJimFlavorRepository, SlimJimFlavorRepository>();
        builder.Services.AddScoped<IVoteRepository, VoteRepository>();
        
        //Register Services
        builder.Services.AddScoped<ISlimJimFlavorService, SlimJimFlavorService>();
        builder.Services.AddScoped<IVoteService, VoteService>();
        
        //Register Controllers
        builder.Services.AddControllers();
        
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        
        var app = builder.Build();
        
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.UseHttpsRedirection();

        app.UseAuthorization();

        // Map Controllers (this enables your API endpoints)
        app.MapControllers();
        
        // Only add the port configuration for production (Docker)
        if (!app.Environment.IsDevelopment())
        {
            var port = Environment.GetEnvironmentVariable("PORT") ?? "80";
            app.Urls.Add($"http://0.0.0.0:{port}");
        }

        app.Run();

    }
}