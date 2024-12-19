using DAL;
using GameBrain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages;

public class PlayGame(IGameRepository gameRepository) : PageModel
{
    [BindProperty(SupportsGet = true)]
    public string? Error { get; set; }
    
    [BindProperty(SupportsGet = true)]
    public string? Message { get; set; }
    public string GamePassword { get; set; } = default!;
    public int BoardWidth { get; set; }
    public int BoardHeight { get; set; }
    public string[][] GameBoard { get; set; } = default!;
    public SlidingGrid Grid { get; set; } = default!;
    [BindProperty] public int MadeMoves { get; set; }
    [BindProperty(SupportsGet = true)] public string? UserName { get; set; }
    
    [BindProperty(SupportsGet = true)] public EGamePiece? PieceWon { get; set; }
    [BindProperty] public EGamePiece NextMoveBy { get; set; }
    
    [BindProperty(SupportsGet = true)] public EGamePiece YourFigure { get; set; }
    
    [BindProperty(SupportsGet = true)] public int Id { get; set; }
    
    
    public IActionResult OnGet()
    {
        if (string.IsNullOrEmpty(UserName)) return RedirectToPage("./Index", new { error = "No username provided." });
        ViewData["UserName"] = UserName;
        
        LoadGameState(Id);
        return Page();
    }
    
    private void LoadGameState(int gameId)
    {
        var gameState = gameRepository.GetGameStateById(gameId);
        
        BoardWidth = gameState!.GameConfiguration.BoardWidth;
        BoardHeight = gameState.GameConfiguration.BoardHeight;
        GameBoard = ConvertBoard(gameState.GameBoard);
        Grid = gameState.SlidingGrid;
        MadeMoves = gameState.MadeMoves;
        NextMoveBy = gameState.NextMoveBy;
        GamePassword = gameState.GamePassword;
    }
    
    private string[][] ConvertBoard(EGamePiece[][] gamePieces)
    {
        return gamePieces.Select(row => row.Select(cell => cell.ToString()).ToArray()).ToArray();
    }
    
    public bool IsGridCell(int x, int y)
    {
        return y >= Grid.StartCol && y <= Grid.EndCol &&
               x >= Grid.StartRow && x <= Grid.EndRow;
    }

    public IActionResult OnPost(int? x, int? y, string? direction)
    {
        var gameState = gameRepository.GetGameStateById(Id);
        var gameInstance = new TicTacTwoBrain(gameState!, gameState!.SlidingGrid);
        
        if (YourFigure != gameInstance.GameState.NextMoveBy)
        {
            return RedirectToPage("./PlayGame", new { UserName, Id, YourFigure, Error = "It is not your turn!" });
        }
        
        if (direction != null)
        {
            try
            {
                gameInstance.MoveAGrid(direction);
                PieceWon = gameInstance.CheckWinCondition(gameInstance.GameState.NextMoveBy);
            }
            catch (Exception e)
            {
                return RedirectToPage("./PlayGame", new { UserName, Id, YourFigure, Error = e.Message });
            }
            
        }
        
        if (x != null && y != null)
        {
            if (gameState.SlidingGrid is { GridCenterX: 0, GridCenterY: 0 })
            {
                TempData.Remove("SelectedX");
                TempData.Remove("SelectedY");
                if (!gameInstance.PlaceAGrid(gameState.SlidingGrid, x.Value, y.Value))
                {
                    
                    return RedirectToPage("./PlayGame", new { UserName, Id, YourFigure,
                        Error = "Invalid coordinates. Grid is out if board bounds!" });
                }
                
                gameRepository.SaveGame(gameInstance.GameState);
                PieceWon = gameInstance.CheckWinCondition(gameInstance.GameState.NextMoveBy);
                return RedirectToPage("./PlayGame", new { UserName, Id, YourFigure, PieceWon});
            }
            
            if (TempData["SelectedX"] is int selectedX && TempData["SelectedY"] is int selectedY)
            {
                try
                {
                    gameInstance.ChangePiecePosition(x.Value, y.Value, selectedX, selectedY);
                    TempData.Remove("SelectedX");
                    TempData.Remove("SelectedY");
                }
                catch (Exception e)
                {
                    TempData.Remove("SelectedX");
                    TempData.Remove("SelectedY");
                    return RedirectToPage("./PlayGame", new { UserName, Id, YourFigure, Error = e.Message });
                }
                
                gameRepository.SaveGame(gameInstance.GameState);
                PieceWon = gameInstance.CheckWinCondition(gameInstance.GameState.NextMoveBy);
                return RedirectToPage("./PlayGame", 
                    new { UserName, Id, YourFigure, PieceWon, Message = "Figure successfully moved!" });
            }

            if (gameState.GameBoard[x.Value][y.Value] != EGamePiece.Empty &&
                gameState.GameBoard[x.Value][y.Value] == YourFigure)
            {
                TempData["SelectedX"] = x.Value;
                TempData["SelectedY"] = y.Value;
                
                return RedirectToPage("./PlayGame", 
                    new { UserName, Id, YourFigure, Message = "Choose where you would like to move the figure." });
            }

            try
            {
                TempData.Remove("SelectedX");
                TempData.Remove("SelectedY");
                gameInstance.MakeAMove(x.Value, y.Value);
            }
            catch (Exception e)
            {
                return RedirectToPage("./PlayGame", new { UserName, Id, YourFigure, Error = e.Message });
            }
        }
        gameRepository.SaveGame(gameInstance.GameState);
        PieceWon = gameInstance.CheckWinCondition(gameInstance.GameState.NextMoveBy);
        return RedirectToPage("./PlayGame", new { UserName, Id, YourFigure, PieceWon });
    }
}