using System.Threading.Tasks;

namespace Maui_tictactoe.Models;

public class GameHistory : IGameHistory
{
    private readonly MorpionDatabase _db;
    private ScoreEntity _currentScore;

    public int Victoires => _currentScore?.Victoires ?? 0;
    public int Defaites => _currentScore?.Defaites ?? 0;
    public int Nuls => _currentScore?.Nuls ?? 0;

    public GameHistory(MorpionDatabase db)
    {
        _db = db;
        Task.Run(async () => await ChargerScoresDepuisDB());
    }

    private async Task ChargerScoresDepuisDB()
    {
        _currentScore = await _db.GetScoreAsync();
    }

    public void AjouterVictoire()
    {
        if (_currentScore != null)
        {
            _currentScore.Victoires++;
            Task.Run(() => _db.UpdateScoreAsync(_currentScore));
        }
    }

    public void AjouterDefaite()
    {
        if (_currentScore != null)
        {
            _currentScore.Defaites++;
            Task.Run(() => _db.UpdateScoreAsync(_currentScore));
        }
    }

    public void AjouterNul()
    {
        if (_currentScore != null)
        {
            _currentScore.Nuls++;
            Task.Run(() => _db.UpdateScoreAsync(_currentScore));
        }
    }
}