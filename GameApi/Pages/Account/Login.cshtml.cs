using System.Security.Claims;
using GameApi.Data;
using GameApi.Models;
using GameApi.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GameApi.Pages.Account;

[IgnoreAntiforgeryToken]
public class Login : PageModel
{
    [BindProperty] public string? Username { get; set; }
    [BindProperty] public string? Password { get; set; }
    public string Error { get; set; }
    
    public async void OnGet()
    {

    }

    public async Task<IActionResult> OnPostAsync([FromServices] PlayerService playerService)
    {
        var player = await playerService.GetPlayerByLoginAndPassword(Username, Password);
        if (player != null)
        {
            await SingIn(player);
            if (player.Role == "Admin")
                return Redirect("/Admin");
            else
                return Redirect("/User");
        }
        else
        {
            Error = "Invalid username or password";
            return Page();
        }
    }

    private async Task<Player?> GetPlayer(ApplicationDbContext db)
    {
        return await db.Players.FirstOrDefaultAsync(x => x.Password == Password && x.Login == Username);
    }
    public async Task SingIn(Player player)
    {
        
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, player.Login!),
            new Claim(ClaimTypes.Role, player.Role!)
        };


        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity));
    }
}