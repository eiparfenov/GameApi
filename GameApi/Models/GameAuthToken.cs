namespace GameApi.Models;

public class GameAuthToken: IHasId
{
    public int Id { get; set; }
    public Guid Token { get; set; }
    public Player? Player { get; set; }
    public int PlayerId { get; set; }
}