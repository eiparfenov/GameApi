using GameApi.Data;
using GameApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameApi.Controllers;
public class TokenController : Controller
{
    
    public async Task<IActionResult> SetScore([FromQuery] Guid token, [FromQuery] string level, [FromQuery] int result, [FromQuery] string companyName, [FromQuery] string gameName, [FromServices] ApplicationDbContext applicationDbContext)
    {
        var company = await applicationDbContext.Companies.FirstAsync(c => c.Name == companyName);
        var game = await applicationDbContext.Games.FirstAsync(g => g.CompanyId == company.Id && g.Name == gameName);
        var authToken = await applicationDbContext.GameAuthTokens.FirstAsync(t => t.Token == token);
        var player = await applicationDbContext.Players.FindAsync(authToken.PlayerId);
        var score = new Score(){GameId = game.Id, PlayerId = player.Id, Level = level, Result = result};
        await applicationDbContext.AddAsync(score);
        await applicationDbContext.SaveChangesAsync();
        return Ok();
    }

    public string Index()
    {
        return Guid.NewGuid().ToString();
    }
}