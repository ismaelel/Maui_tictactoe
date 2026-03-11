using Xunit;
using Maui_tictactoe.ViewModels;
using System.Threading.Tasks;

namespace Maui_tictactoe.Tests;

public class JeuViewModelTests
{
    [Fact]
    public void Constructeur_DoitInitialiserLaGrilleEtLeTexte()
    {
        // Arrange
        var fakeHistory = new FakeGameHistoryTest();

        // Act
        var viewModel = new JeuViewModel(fakeHistory);

        //Assert
        Assert.Equal(9, viewModel.Cases.Count);
        Assert.Equal("À ton tour de jouer !", viewModel.StatutTexte);
        Assert.Equal(0, viewModel.VictoiresUI);
    }

    [Fact]
    public async Task JouerCommand_SurUneCaseVide_DoitPlacerUneCroixEtLaisserLeBotJouer()
    {
        // Arrange
        var fakeHistory = new FakeGameHistoryTest();
        var viewModel = new JeuViewModel(fakeHistory);
        var caseCible = viewModel.Cases[0];

        // Act
        await viewModel.JouerCommand.ExecuteAsync(caseCible);

        // Assert
        Assert.Equal("X", caseCible.Symbole);
        Assert.Equal("À ton tour de jouer !", viewModel.StatutTexte);

        int nombreDeO = viewModel.Cases.Count(c => c.Symbole == "O");
        Assert.Equal(1, nombreDeO);
    }

    [Fact]
    public async Task JouerCommand_SurUneCaseDejaPrise_NeDoitRienFaire()
    {
        // Arrange
        var fakeHistory = new FakeGameHistoryTest();
        var viewModel = new JeuViewModel(fakeHistory);
        var caseCible = viewModel.Cases[0];

        // Act
        await viewModel.JouerCommand.ExecuteAsync(caseCible);
        await Task.Delay(1000);

        var symboleApresPremierCoup = caseCible.Symbole;

        await viewModel.JouerCommand.ExecuteAsync(caseCible);

        // Assert
        Assert.Equal(symboleApresPremierCoup, caseCible.Symbole);
    }

    [Fact]
    public async Task RejouerCommand_DoitViderLaGrille()
    {
        // Arrange
        var fakeHistory = new FakeGameHistoryTest();
        var viewModel = new JeuViewModel(fakeHistory);
        var caseCible = viewModel.Cases[0];
        await viewModel.JouerCommand.ExecuteAsync(caseCible);

        // Act
        viewModel.RejouerCommand.Execute(null);

        // Assert
        Assert.Equal("", caseCible.Symbole);
        Assert.Equal("À ton tour de jouer !", viewModel.StatutTexte);
    }

    [Fact]
    public async Task ApresLeTourDuBot_LeTexteDoitIndiquerLeTourDuJoueur()
    {
        // Arrange
        var fakeHistory = new FakeGameHistoryTest();
        var viewModel = new JeuViewModel(fakeHistory);
        var caseCible = viewModel.Cases[0];

        // Act
        await viewModel.JouerCommand.ExecuteAsync(caseCible);

        // Assert
        Assert.Equal("À ton tour de jouer !", viewModel.StatutTexte);
    }

    [Fact]
    public async Task FinDePartie_DoitAfficherUnTexteDeFinValide()
    {
        // Arrange
        var fakeHistory = new FakeGameHistoryTest();
        var viewModel = new JeuViewModel(fakeHistory);

        // Act
        while (viewModel.StatutTexte == "À ton tour de jouer !" || viewModel.StatutTexte == "Le Robot réfléchit...")
        {
            var caseVide = viewModel.Cases.FirstOrDefault(c => c.Symbole == "");

            if (caseVide != null)
            {
                await viewModel.JouerCommand.ExecuteAsync(caseVide);
            }
            else
            {
                break; 
            }
        }

        // Assert
        bool isTexteFinValide =
            viewModel.StatutTexte == "Bravo ! Tu as gagné !" ||
            viewModel.StatutTexte == "Aïe... Le Bot a gagné !" ||
            viewModel.StatutTexte == "Match nul !";

        Assert.True(isTexteFinValide, $"Erreur : Le texte final affiché est '{viewModel.StatutTexte}'");
    }
}