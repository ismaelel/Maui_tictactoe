namespace Maui_tictactoe;

public class Jeu
{
    public Plateau PlateauJeu { get; private set; }
    public char JoueurActuel { get; private set; }
    public char Resultat { get; private set; } // ' ', 'X', 'O', 'N'

    public Jeu()
    {
        PlateauJeu = new Plateau();
        JoueurActuel = 'X';
        Resultat = ' ';
    }

    public bool JouerUnCoup(int ligne, int colonne)
    {
        // partie finie
        if (Resultat != ' ') return false;

        bool coupValide = PlateauJeu.PlacerCoup(ligne, colonne, JoueurActuel);

        if (coupValide)
        {
            Resultat = PlateauJeu.VerifierFinDePartie();

            if (Resultat == ' ')
            {
                JoueurActuel = (JoueurActuel == 'X') ? 'O' : 'X';
            }
            return true; // c bon
        }

        return false; // Case occupée
    }

    public void RelancerPartie()
    {
        PlateauJeu.Initialiser();
        JoueurActuel = 'X';
        Resultat = ' ';
    }
}