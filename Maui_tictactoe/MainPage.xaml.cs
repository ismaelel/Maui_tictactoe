using Microsoft.Maui.Controls;

namespace Maui_tictactoe;

public partial class MainPage : ContentPage
{
    private Jeu _moteurDeJeu;

    public MainPage()
    {
        InitializeComponent();
        _moteurDeJeu = new Jeu();
        MettreAJourTexteStatut();
    }

    private void OnCaseClicked(object sender, EventArgs e)
    {
        var boutonClique = (Button)sender;

        int ligne = Grid.GetRow(boutonClique);
        int colonne = Grid.GetColumn(boutonClique);

        bool coupJoue = _moteurDeJeu.JouerUnCoup(ligne, colonne);

        if (coupJoue)
        {
            SynchroniserGrilleVisuelle();
            MettreAJourTexteStatut();
        }
    }

    private void OnRejouerClicked(object sender, EventArgs e)
    {
        _moteurDeJeu.RelancerPartie();
        SynchroniserGrilleVisuelle();
        MettreAJourTexteStatut();
    }

    private void SynchroniserGrilleVisuelle()
    {
        foreach (var element in GrilleMorpion.Children)
        {
            if (element is Button btn)
            {
                int r = Grid.GetRow(btn);
                int c = Grid.GetColumn(btn);
                char symbole = _moteurDeJeu.PlateauJeu.GetCase(r, c);

                btn.Text = symbole == ' ' ? "" : symbole.ToString();
            }
        }
    }

    private void MettreAJourTexteStatut()
    {
        if (_moteurDeJeu.Resultat == 'N')
            StatutLabel.Text = "Match nul !";
        else if (_moteurDeJeu.Resultat != ' ')
            StatutLabel.Text = $"Bravo ! {_moteurDeJeu.Resultat} a gagné !";
        else
            StatutLabel.Text = $"Tour du joueur : {_moteurDeJeu.JoueurActuel}";
    }
}