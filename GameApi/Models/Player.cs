namespace GameApi.Models;

public class Player: IHasId
{
    public int Id { get; set; }
    public string? Login { get; set; }
    public string? Password { get; set; }
    public string? Status { get; set; }
    public string? Role { get; set; }
}