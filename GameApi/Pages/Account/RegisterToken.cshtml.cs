using System.Security.Claims;
using GameApi.Data;
using GameApi.Models;
using GameApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GameApi.Pages.Account;

public class RegisterToken : PageModel
{
    public async void OnGetAsync([FromQuery] string token, [FromServices] ApplicationDbContext applicationDbContext, [FromServices] PlayerService playerService)
    {
        if (!User.Identity.IsAuthenticated) RedirectToPage("login");
        var player = await playerService.GetPlayerByLogin(User.Claims.First(c => c.Type == ClaimTypes.Name).Value);
        var authToken = new GameAuthToken() {PlayerId = player.Id, Token = Guid.Parse(token)};
        await applicationDbContext.GameAuthTokens.AddAsync(authToken);
        await applicationDbContext.SaveChangesAsync();
        RedirectToPage("login");
    }
}