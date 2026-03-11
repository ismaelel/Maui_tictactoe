using SQLite;

namespace Maui_tictactoe.Models;

public class ScoreEntity
{
    [PrimaryKey]
    public int Id { get; set; } = 1; 

    public int Victoires { get; set; }
    public int Defaites { get; set; }
    public int Nuls { get; set; }
}