using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Maui_tictactoe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maui_tictactoe.ViewModels;

public partial class CaseViewModel : ObservableObject
{
    [ObservableProperty]
    private string _symbole = string.Empty;

    public int Ligne { get; set; }
    public int Colonne { get; set; }
}

public partial class JeuViewModel : ObservableObject
{
    private readonly Jeu _moteurDeJeu;
    private readonly IGameHistory _historiqueModèle;

    [ObservableProperty] private string _statutTexte = string.Empty;

    [ObservableProperty] private int _victoiresUI;
    [ObservableProperty] private int _defaitesUI;
    [ObservableProperty] private int _nulsUI;

    public List<CaseViewModel> Cases { get; set; }

    public JeuViewModel(IGameHistory historiqueInjecte)
    {
        _moteurDeJeu = new Jeu();
        _historiqueModèle = historiqueInjecte;
        Cases = new List<CaseViewModel>();

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Cases.Add(new CaseViewModel { Ligne = i, Colonne = j, Symbole = "" });
            }
        }

        ActualiserScoresUI();
        MettreAJourTexteStatut();
    }

    [RelayCommand]
    private async Task Jouer(CaseViewModel caseCliquee)
    {
        if (_moteurDeJeu.JoueurActuel == 'O' || _moteurDeJeu.Resultat != ' ')
            return;

        bool coupJoue = _moteurDeJeu.JouerUnCoup(caseCliquee.Ligne, caseCliquee.Colonne);

        if (coupJoue)
        {
            caseCliquee.Symbole = "X";
            VerifierFinDePartie();

            if (_moteurDeJeu.Resultat == ' ')
            {
                await FaireJouerRobot();
            }
        }
    }

    private async Task FaireJouerRobot()
    {
        StatutTexte = "Le Robot réfléchit...";
        await Task.Delay(800);

        var casesVides = Cases.Where(c => string.IsNullOrEmpty(c.Symbole)).ToList();

        if (casesVides.Any())
        {
            var random = new Random();
            var caseChoisie = casesVides[random.Next(casesVides.Count)];

            _moteurDeJeu.JouerUnCoup(caseChoisie.Ligne, caseChoisie.Colonne);
            caseChoisie.Symbole = "O";

            VerifierFinDePartie();

            if (_moteurDeJeu.Resultat == ' ')
            {
                MettreAJourTexteStatut();
            }
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
            StatutTexte = _moteurDeJeu.JoueurActuel == 'X' ? "À ton tour de jouer !" : "Le robot joue...";
    }

    private void VerifierFinDePartie()
    {
        if (_moteurDeJeu.Resultat == 'X')
        {
            StatutTexte = "Bravo ! Tu as gagné !";
            _historiqueModèle.AjouterVictoire();
        }
        else if (_moteurDeJeu.Resultat == 'O')
        {
            StatutTexte = "Aïe... Le Bot a gagné !";
            _historiqueModèle.AjouterDefaite();
        }
        else if (_moteurDeJeu.Resultat == 'N')
        {
            StatutTexte = "Match nul !";
            _historiqueModèle.AjouterNul();
        }

        ActualiserScoresUI();
    }

    private void ActualiserScoresUI()
    {
        VictoiresUI = _historiqueModèle.Victoires;
        DefaitesUI = _historiqueModèle.Defaites;
        NulsUI = _historiqueModèle.Nuls;
    }
}