namespace GameApi.Models;

public class Score: IHasId
{
    public int Id { get; set; }
    public int Result { get; set; }
    public Player? Player { get; set; }
    public int PlayerId { get; set; }
    public Game? Game { get; set; }
    public int GameId { get; set; }
    public string? Level { get; set; }
}