using Maui_tictactoe.ViewModels;

namespace Maui_tictactoe;

public partial class MainPage : ContentPage
{
    public MainPage(JeuViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}