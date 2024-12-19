using DAL;
using GameBrain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Pages;
public class NewGame(IConfigRepository configRepository, IGameTypeRepository gameTypeRepository, IGameRepository gameRepository)
    : PageModel
{
    [BindProperty(SupportsGet = true)]
    public string UserName { get; set; } = default!;
    public SelectList ConfigSelectList { get; set; } = default!;
    public SelectList GameTypeList { get; set; } = default!;
    
    [BindProperty]
    public int GameTypeId { get; set; }
    
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

        
        var typeSelectListData = gameTypeRepository.GetGameTypeNames()
            .Select(kv => new { id = kv.Key, value = kv.Value })
            .ToList();

        GameTypeList = new SelectList(typeSelectListData, "id", "value");
        
        return Page();
    }

    public IActionResult OnPost()
    {
        var chosenConfig = configRepository.GetGameConfigurationById(ConfigurationId);
        var chosenGameType = gameTypeRepository.GetGameTypeById(GameTypeId);
        
        var gridConstruct = new SlidingGrid(chosenConfig);
        var gameInstance = new TicTacTwoBrain(chosenConfig, gridConstruct)
        {
            GameState =
            {
                GameType = chosenGameType.Id
            }
        };
        
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