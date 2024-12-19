using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Domain;

namespace WebApp.Pages_SaveGame
{
    public class CreateModel(IGameRepository gameRepository, IConfigRepository configRepository,
        AppDbContext context) : PageModel
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
            
            var confSelectListData = configRepository.GetConfigurationNames()
                .Select(kv => new { id = kv.Key, value = kv.Value })
                .ToList();
            
            ViewData["ConfigurationId"] = new SelectList(confSelectListData, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public SaveGame SaveGame { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            context.SaveGames.Add(SaveGame);
            await context.SaveChangesAsync();

            return RedirectToPage("./Index", new { UserName });
        }
    }
}
