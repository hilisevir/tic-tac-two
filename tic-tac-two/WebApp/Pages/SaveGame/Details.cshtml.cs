using DAL;
using GameBrain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.SaveGame
{
    public class DetailsModel(IGameRepository gameRepository) : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string? UserName { get; set; }
        public GameState SaveGame { get; set; } = default!;

        public Task<IActionResult> OnGetAsync(int id)
        {
            if (string.IsNullOrEmpty(UserName))
            {
                return Task.FromResult<IActionResult>(RedirectToPage("/Index", new { error = "No username provided." }));
            }

            ViewData["UserName"] = UserName;
            

            var saveGame  = gameRepository.GetGameStateById(id);

            SaveGame = saveGame!;

            return Task.FromResult<IActionResult>(Page());
        }
    }
}
