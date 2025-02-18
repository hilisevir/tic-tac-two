using DAL;
using GameBrain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Pages;
public class NewGame(IConfigRepository configRepository, IGameRepository gameRepository)
    : PageModel
{
    [BindProperty(SupportsGet = true)]
    public string UserName { get; set; } = default!;
    public SelectList ConfigSelectList { get; set; } = default!;
    
    [BindProperty]
    public int ConfigurationId { get; set; }
    
    [BindProperty]
    public string? GameName { get; set; }

    
    public IActionResult OnGet()
    {
        if (string.IsNullOrEmpty(UserName)) return RedirectToPage("./Index", new { error = "No username provided." });
        
        ViewData["UserName"] = UserName;

        var confSelectListData = configRepository.GetConfigurationNames()
            .Select(kv => new { id = kv.Key, value = kv.Value })
            .ToList();

        ConfigSelectList = new SelectList(confSelectListData, "id", "value");
        
        return Page();
    }

    public IActionResult OnPost()
    {
        var chosenConfig = configRepository.GetGameConfigurationById(ConfigurationId);
        
        var gridConstruct = new SlidingGrid(chosenConfig);
        var gameInstance = new TicTacTwoBrain(chosenConfig, gridConstruct);
        
        GameName = GameName?.Trim();
        if (string.IsNullOrWhiteSpace(GameName))
        {
            GameName = FileHelper.GetUniqueGameName();
        }
        
        gameInstance.GameState.Name = GameName!;
        
        var gameState = gameInstance.GameState;
        gameRepository.SaveGame(gameState);
        
        TempData.Remove("SelectedX");
        TempData.Remove("SelectedY");
        
        return RedirectToPage("./PlayGame", new
        {
            gameState.Id,
            UserName,
            YourFigure = EGamePiece.X
        });
    }
}