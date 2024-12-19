using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Domain;
using GameBrain;

namespace WebApp.Pages_Configuration
{
    public class CreateModel(IConfigRepository configRepository) : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string? UserName { get; set; }
        
        public IActionResult OnGet()
        {
            if (string.IsNullOrEmpty(UserName))
            {
                return RedirectToPage("/Index", new { error = "No username provided." });
            }

            ViewData["UserName"] = UserName;
            
            return Page();
        }

        [BindProperty]
        public Configuration Configuration { get; set; } = default!;

        
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Task.FromResult<IActionResult>(Page());
            }

            var gameConf = new GameConfiguration()
            {
                Name = Configuration.Name,
                BoardWidth = Configuration.BoardWidth,
                BoardHeight = Configuration.BoardHeight,
                GridWidth = Configuration.GridWidth,
                GridHeight = Configuration.GridHeight,
                WinCondition = Configuration.WinCondition,
                MovePieceAfterNMoves = Configuration.MovePieceAfterNMoves,
                Player1PieceAmount = Configuration.Player1PieceAmount,
                Player2PieceAmount = Configuration.Player2PieceAmount
            };
            configRepository.SaveConfiguration(gameConf);

            return Task.FromResult<IActionResult>(RedirectToPage("./Index",new { UserName }));
        }
    }
}
