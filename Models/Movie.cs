using Newtonsoft.Json;

namespace VectorMovieRecommendation.Models;
public class Movie
{
    [JsonProperty("id")]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public float[] Vector { get; set; } = new float[1025];
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int ReleaseYear { get; set; }
    public List<string> Genres { get; set; } = new List<string>();
    public double Rating { get; set; }
}
