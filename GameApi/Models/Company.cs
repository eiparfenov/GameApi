namespace GameApi.Models;

public class Company: IHasId
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}