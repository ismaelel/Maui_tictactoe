namespace Maui_tictactoe.Models; // Vérifie bien ton namespace

public class Plateau
{
    private char[,] _grille;
    public const int Taille = 3;

    public Plateau()
    {
        _grille = new char[Taille, Taille];
        Initialiser();
    }

    public void Initialiser()
    {
        for (int i = 0; i < Taille; i++)
            for (int j = 0; j < Taille; j++)
                _grille[i, j] = ' ';
    }

    // lire une case 
    public char GetCase(int ligne, int colonne)
    {
        return _grille[ligne, colonne];
    }

    public bool PlacerCoup(int ligne, int colonne, char symboleJoueur)
    {
        if (ligne < 0 || ligne >= Taille || colonne < 0 || colonne >= Taille) return false;
        if (_grille[ligne, colonne] != ' ') return false;

        _grille[ligne, colonne] = symboleJoueur;
        return true;
    }

    public char VerifierFinDePartie()
    {
        for (int i = 0; i < 3; i++)
        {
            if (_grille[i, 0] != ' ' && _grille[i, 0] == _grille[i, 1] && _grille[i, 1] == _grille[i, 2]) return _grille[i, 0];
            if (_grille[0, i] != ' ' && _grille[0, i] == _grille[1, i] && _grille[1, i] == _grille[2, i]) return _grille[0, i];
        }

        if (_grille[1, 1] != ' ')
        {
            if (_grille[0, 0] == _grille[1, 1] && _grille[1, 1] == _grille[2, 2]) return _grille[1, 1];
            if (_grille[0, 2] == _grille[1, 1] && _grille[1, 1] == _grille[2, 0]) return _grille[1, 1];
        }

        bool plein = true;
        foreach (char c in _grille) if (c == ' ') plein = false;

        return plein ? 'N' : ' ';
    }
}