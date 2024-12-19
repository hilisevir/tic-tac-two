using DAL;
using GameBrain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.SaveGame
{
    public class IndexModel(IGameRepository gameRepository) : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string? UserName { get; set; }
        

        public IList<GameState> SaveGame { get;set; } = new List<GameState>();

        public Task<IActionResult> OnGetAsync()
        {
            var gameDict = gameRepository.GetGameNames();
            foreach (var save in gameDict)
            {
                var saveGame = gameRepository.GetGameStateById(save.Key);
                SaveGame.Add(saveGame!);
            }
            
            if (string.IsNullOrEmpty(UserName))
            {
                return Task.FromResult<IActionResult>(RedirectToPage("/Index", new { error = "No username provided." }));
            }

            ViewData["UserName"] = UserName;
            return Task.FromResult<IActionResult>(Page());
        }
    }
}
