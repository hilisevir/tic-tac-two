using DAL;
using GameBrain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.SaveGame
{
    public class DeleteModel(IGameRepository gameRepository) : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string? UserName { get; set; }

        [BindProperty]
        public GameState SaveGame { get; set; } = default!;

        public Task<IActionResult> OnGetAsync(int id)
        {
            if (string.IsNullOrEmpty(UserName))
            {
                return Task.FromResult<IActionResult>(RedirectToPage("/Index", new { error = "No username provided." }));
            }

            ViewData["UserName"] = UserName;

            var saveGame = gameRepository.GetGameStateById(id);

            SaveGame = saveGame!;

            return Task.FromResult<IActionResult>(Page());
        }

        public Task<IActionResult> OnPostAsync(int id)
        {

            var saveGame = gameRepository.GetGameStateById(id);
            
            gameRepository.DeleteGame(saveGame!.Id);
            
            return Task.FromResult<IActionResult>(RedirectToPage("./Index", new { UserName }));
        }
    }
}
