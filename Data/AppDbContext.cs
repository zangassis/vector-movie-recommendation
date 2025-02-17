using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using VectorMovieRecommendation.Models;
namespace VectorMovieRecommendation.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Movie> Movies { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #pragma warning disable EF9103 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
        modelBuilder.Entity<Movie>()
            .Property(e => e.Vector)
            .IsVector(DistanceFunction.Cosine, 1025);
        #pragma warning restore EF9103 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
    }
}