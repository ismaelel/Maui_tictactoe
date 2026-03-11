using SQLite;
using System.Threading.Tasks;

namespace Maui_tictactoe.Models;

public class MorpionDatabase
{
    private SQLiteAsyncConnection _database;

    private async Task Init()
    {
        if (_database is not null) return;

    _database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
    await _database.CreateTableAsync<ScoreEntity>();
    }

    public async Task<ScoreEntity> GetScoreAsync()
    {
        await Init();
        var score = await _database.Table<ScoreEntity>().FirstOrDefaultAsync(s => s.Id == 1);

        // Si c'est la toute première partie, on crée la ligne à 0
        if (score == null)
        {
            score = new ScoreEntity { Id = 1, Victoires = 0, Defaites = 0, Nuls = 0 };
            await _database.InsertAsync(score);
        }
        return score;
    }

    public async Task UpdateScoreAsync(ScoreEntity score)
    {
        await Init();
        await _database.UpdateAsync(score);
    }
}