using GameApi.Data;
using GameApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GameApi.Services;

public class ScoreService
{
    private readonly ApplicationDbContext _db;

    public ScoreService(ApplicationDbContext db)
    {
        _db = db;
    }

    public IGrouping<string, Score>[] GetScores(Player player)
    {
        var scores = _db.Scores.Include(x => x.Game)
            .Where(x => x.PlayerId == player.Id)
            .OrderBy(x => x.Result)
            .GroupBy(x => x.Game.Name);
        return scores.ToArray()!;
    }
}