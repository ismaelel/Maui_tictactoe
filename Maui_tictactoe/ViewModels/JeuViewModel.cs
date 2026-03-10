using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Maui_tictactoe.Models;

namespace Maui_tictactoe.ViewModels;

public partial class CaseViewModel : ObservableObject
{
    [ObservableProperty]
    private string _symbole;

    public int Ligne { get; set; }
    public int Colonne { get; set; }
}

public partial class JeuViewModel : ObservableObject
{
    private readonly Jeu _moteurDeJeu;

    [ObservableProperty]
    private string _statutTexte;

    public List<CaseViewModel> Cases { get; set; }

    public JeuViewModel()
    {
        _moteurDeJeu = new Jeu();
        Cases = new List<CaseViewModel>();

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Cases.Add(new CaseViewModel { Ligne = i, Colonne = j, Symbole = "" });
            }
        }
        MettreAJourTexteStatut();
    }
   
    [RelayCommand]
    private void Jouer(CaseViewModel caseCliquee)
    {
        bool coupJoue = _moteurDeJeu.JouerUnCoup(caseCliquee.Ligne, caseCliquee.Colonne);

        if (coupJoue)
        {
            char symboleModèle = _moteurDeJeu.PlateauJeu.GetCase(caseCliquee.Ligne, caseCliquee.Colonne);
            caseCliquee.Symbole = symboleModèle == ' ' ? "" : symboleModèle.ToString();

            MettreAJourTexteStatut();
        }
    }

    [RelayCommand]
    private void Rejouer()
    {
        _moteurDeJeu.RelancerPartie();

        foreach (var c in Cases)
        {
            c.Symbole = "";
        }

        MettreAJourTexteStatut();
    }

    private void MettreAJourTexteStatut()
    {
        if (_moteurDeJeu.Resultat == 'N')
            StatutTexte = "Match nul !";
        else if (_moteurDeJeu.Resultat != ' ')
            StatutTexte = $"Bravo ! {_moteurDeJeu.Resultat} a gagné !";
        else
            StatutTexte = $"Tour du joueur : {_moteurDeJeu.JoueurActuel}";
    }
}