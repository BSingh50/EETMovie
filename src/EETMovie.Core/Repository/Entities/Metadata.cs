namespace EETMovie.Core.Repository.Entities;

public class Metadata
{
    public int MovieId { get; set; }
    public string Title { get; set; }
    public string Language { get; set; }
    public string Duration { get; set; }
    public int ReleaseYear { get; set; }
    
    public override string ToString()
    {
        return $"{MovieId},{Title},{Language},{Duration},{ReleaseYear}";
    }
}