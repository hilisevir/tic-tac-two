using DAL;
using GameBrain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages;

public class LoadGame(IGameRepository gameRepository) : PageModel
{
    [BindProperty(SupportsGet = true)]
    public string? Error { get; set; }
    
    [BindProperty(SupportsGet = true)]
    public int Id { get; set; }
    
    [BindProperty(SupportsGet = true)]
    public string? UserName { get; set; }
    
    [BindProperty]
    public EGamePiece YourFigure { get; set; }
    
    [BindProperty]
    public string? GamePassword { get; set; }
    
    public IActionResult OnGet()
    {
        if (string.IsNullOrEmpty(UserName))
        {
            return RedirectToPage("/Index", new { error = "No username provided." });
        }
        ViewData["UserName"] = UserName;
        
        
        return Page();
    }

    public IActionResult OnPost()
    {
        
        var gameState = gameRepository.GetGameStateById(Id);
        
        TempData.Remove("SelectedX");
        TempData.Remove("SelectedY");
        
        var figure = Request.Form["figure"];
        if (string.IsNullOrEmpty(figure))
        {
            return RedirectToPage("/LoadGame", new { UserName, Id, error = "No figure selected." });
        }
        YourFigure = figure == "X" ? EGamePiece.X : EGamePiece.O;
        
        return GamePassword != gameState!.GamePassword ? 
            RedirectToPage("/LoadGame", new { UserName, Id, error = "Game password does not match." }) : 
            RedirectToPage("/PlayGame", new { UserName, Id, YourFigure });
    }
}