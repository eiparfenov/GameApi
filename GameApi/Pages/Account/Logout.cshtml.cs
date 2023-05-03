using GameApi.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GameApi.Pages.Account;

[IgnoreAntiforgeryToken]
public class Logout : PageModel
{
    public IActionResult OnGet()
    {
        if (!HttpContext.User.Identity.IsAuthenticated)
        {
            return Redirect("login");
        }

        return Page();
    }
    public async Task<IActionResult> OnPostAsync([FromServices] PlayerService playerService)
    {
        await LogOut();
        return Redirect("login");
    }
    public async Task LogOut()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }
}