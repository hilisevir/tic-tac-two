using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain;

namespace WebApp.Pages_SaveGame
{
    public class EditModel(DAL.AppDbContext context) : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string? UserName { get; set; }
        
        [BindProperty]
        public SaveGame SaveGame { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (string.IsNullOrEmpty(UserName))
            {
                return RedirectToPage("/Index", new { error = "No username provided." });
            }

            ViewData["UserName"] = UserName;
            
            if (id == null)
            {
                return NotFound();
            }

            var savegame =  await context.SaveGames.FirstOrDefaultAsync(m => m.Id == id);
            if (savegame == null)
            {
                return NotFound();
            }
            SaveGame = savegame;
            
            ViewData["ConfigurationId"] = new SelectList(context.Configurations, "Id", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            context.Attach(SaveGame).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SaveGameExists(SaveGame.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index", new { UserName });
        }

        private bool SaveGameExists(int id)
        {
            return context.SaveGames.Any(e => e.Id == id);
        }
    }
}
