namespace GameApi.Models;

public class Game: IHasId
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public Company? Company { get; set; }
    public int CompanyId { get; set; }
}