using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Cosmos.Extensions;
using VectorMovieRecommendation.Data;
using VectorMovieRecommendation.Models;

var builder = WebApplication.CreateBuilder(args);

var cosmosConfig = builder.Configuration.GetSection("CosmosDb");
var connectionString = cosmosConfig["AccountEndpoint"];
var accountKey = cosmosConfig["AccountKey"];
var databaseName = cosmosConfig["DatabaseName"];

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseCosmos(connectionString!, accountKey!, databaseName!));

var app = builder.Build();

app.MapGet("/movies", async ([FromServices] AppDbContext db) => await db.Movies.ToListAsync());

app.MapPost("/movies", async ([FromBody] Movie movie, AppDbContext db) =>
{
    db.Movies.Add(movie);
    await db.SaveChangesAsync();
    return Results.Created($"/movies/{movie.Id}", movie);
});

app.MapGet("/movies/vector-movies", async (AppDbContext db) =>
{
    var queryVector = GenerateRandomVector();

    #pragma warning disable EF9103 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
    var movies = await db.Movies
    .OrderBy(m => EF.Functions.VectorDistance(m.Vector, queryVector))
    .Take(5)
    .ToListAsync();
    #pragma warning restore EF9103 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
    
    return Results.Ok(movies);
});

app.MapPost("/movies/seed", async (AppDbContext db) =>
{
    await Seed(db);
    return Results.Ok();
});

static async Task Seed(AppDbContext dbContext)
{
    var sampleMovies = new List<Movie>
            {
                new Movie
                {
                    Title = "Sci-Fi Adventure",
                    Description = "A thrilling space journey.",
                    ReleaseYear = 2022,
                    Genres = new List<string> { "Sci-Fi", "Adventure" },
                    Rating = 8.5,
                    Vector = GenerateRandomVector()
                },
                new Movie
                {
                    Title = "Interstellar Voyage",
                    Description = "A group of explorers travel through a wormhole.",
                    ReleaseYear = 2014,
                    Genres = new List<string> { "Sci-Fi", "Drama" },
                    Rating = 8.6,
                    Vector = GenerateRandomVector()
                },
                new Movie
                {
                    Title = "Futuristic Battle",
                    Description = "A war between humans and AI.",
                    ReleaseYear = 2023,
                    Genres = new List<string> { "Sci-Fi", "Action" },
                    Rating = 8.2,
                    Vector = GenerateRandomVector()
                },
                new Movie
                {
                    Title = "Galactic War",
                    Description = "Intergalactic conflict between empires.",
                    ReleaseYear = 2019,
                    Genres = new List<string> { "Sci-Fi", "Adventure" },
                    Rating = 8.4,
                    Vector = GenerateRandomVector()
                }
            };

    dbContext.Movies.AddRange(sampleMovies);
    await dbContext.SaveChangesAsync();
}

static float[] GenerateRandomVector()
{
    int size = 1536;
    var random = new Random();
    return Enumerable.Range(0, size)
                     .Select(_ => (float)random.NextDouble())
                     .ToArray();
}

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await dbContext.Database.EnsureCreatedAsync();
}

app.Run();