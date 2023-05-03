using System.Security.Claims;
using GameApi.Data;
using GameApi.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace GameApi.Services;

public class PlayerService
{
    private readonly ApplicationDbContext _db;

    public PlayerService(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Player?> GetPlayerByLogin(string? login)
    {
        if (string.IsNullOrEmpty(login)) return null;
        var player = await _db.Players.FirstOrDefaultAsync(x => x.Login == login);
        return player;
    }

    public async Task<Player?> GetPlayerByLoginAndPassword(string? login, string? password)
    {
        var player = await GetPlayerByLogin(login);
        if (player == null) return null;
        if (player.Password != password) return null;
        return player;
    }

    public async Task<Player?> CreatePlayer(string? login, string? password)
    {
        var alreadyCreatedPlayer = await GetPlayerByLogin(login);
        if (alreadyCreatedPlayer != null) return null;
        var player = new Player() { Login = login, Password = password, Role = "User", Status = "" };
        await _db.AddAsync(player);
        await _db.SaveChangesAsync();
        return player;
    }
    

    
}