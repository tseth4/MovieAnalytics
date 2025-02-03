namespace MovieAnalytics.API.DTOs;

public class UserDto
{
    public required string Id { get; set; }
    public  string? Username { get; set; }
    public required string KnownAs { get; set; }
    public string? Token { get; set; }
}