namespace MovieAnalytics.API.DTOs;

public class MemberDto
{
    public int Id { get; set; }
    public string? UserName { get; set; } = "Default";
    public string? KnownAs { get; set; }
}