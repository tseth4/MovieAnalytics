namespace MovieAnalytics.API.Entities;

public class MovieLike
{
    public AppUser SourceUser { get; set; }  = null!;
    public string SourceUserId { get; set; }  = null!;
    public Movie LikedMovie { get; set; }  = null!;
    public string MovieId { get; set; }  = null!;
}