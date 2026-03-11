namespace Maui_tictactoe.Models;

public interface IGameHistory
{
    int Victoires { get; }
    int Defaites { get; }
    int Nuls { get; }

    void AjouterVictoire();
    void AjouterDefaite();
    void AjouterNul();
}