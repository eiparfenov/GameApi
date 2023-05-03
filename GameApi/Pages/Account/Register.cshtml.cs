using System.ComponentModel.DataAnnotations;
using GameApi.Data;
using GameApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GameApi.Pages.Account;
[IgnoreAntiforgeryToken]
public class Register : PageModel
{
    [Required]
    [BindProperty]
    public string Login { get; set; }

    [Required]
    [BindProperty]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }

    public async Task<IActionResult> OnPostAsync([FromServices] ApplicationDbContext db)
    {
        Console.WriteLine(Login);
        if (!ModelState.IsValid) return Page();

        var player = await db.Players.FirstOrDefaultAsync(x => x.Login == Login);
        if (player != null)
        {
            ModelState.AddModelError(string.Empty, "User with this login already exists");
            return Page();
        }

        player = new Player() { Login = Login, Password = Password, Role = "Player"};
        await db.AddAsync(player);
        await db.SaveChangesAsync();
        
        return Redirect("login");
    }
}