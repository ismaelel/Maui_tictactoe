using Maui_tictactoe.Models;

namespace Maui_tictactoe.Tests;

// Un faux historique uniquement pour les tests !
public class FakeGameHistoryTest : IGameHistory
{
    public int Victoires { get; private set; } = 0;
    public int Defaites { get; private set; } = 0;
    public int Nuls { get; private set; } = 0;

    public void AjouterVictoire() => Victoires++;
    public void AjouterDefaite() => Defaites++;
    public void AjouterNul() => Nuls++;
}