namespace allspice.Models;

public class RecipeFan
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Title { get; set; }
    public string Instructions { get; set; }
    public string Img { get; set; }
    public string Category { get; set; }
    public string CreatorId { get; set; }
    public Profile Creator { get; set; }
    public Favorite Favorite { get; set; }
    public Profile Fan { get; set; }
}