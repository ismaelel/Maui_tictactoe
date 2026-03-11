using Microsoft.Maui.Storage;

namespace Maui_tictactoe.Models;

public class GameHistory : IGameHistory
{
    public int Victoires { get; private set; }
    public int Defaites { get; private set; }
    public int Nuls { get; private set; }

    public GameHistory()
    {
        Victoires = Preferences.Get("Score_Victoires", 0);
        Defaites = Preferences.Get("Score_Defaites", 0);
        Nuls = Preferences.Get("Score_Nuls", 0);
    }

    public void AjouterVictoire()
    {
        Victoires++;
        Preferences.Set("Score_Victoires", Victoires);
    }

    public void AjouterDefaite()
    {
        Defaites++;
        Preferences.Set("Score_Defaites", Defaites);
    }

    public void AjouterNul()
    {
        Nuls++;
        Preferences.Set("Score_Nuls", Nuls);
    }
}