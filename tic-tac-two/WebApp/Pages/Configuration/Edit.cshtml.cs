using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Domain;

namespace WebApp.Pages_Configuration
{
    public class EditModel(IConfigRepository configRepository, DAL.AppDbContext context) : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string? UserName { get; set; }
        
        [BindProperty]
        public Configuration Configuration { get; set; } = default!;

        public Task<IActionResult> OnGetAsync(int id)
        {
            if (string.IsNullOrEmpty(UserName))
            {
                return Task.FromResult<IActionResult>(RedirectToPage("/Index", new { error = "No username provided." }));
            }

            ViewData["UserName"] = UserName;
            
            var configuration = configRepository.GetConfigurationById(id);
            
            Configuration = configuration;
            
            return Task.FromResult<IActionResult>(Page());
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            context.Attach(Configuration).State = EntityState.Modified;
            
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConfigurationExists(Configuration.Id))
                {
                    return NotFound();
                }
            }

            return await Task.FromResult<IActionResult>(RedirectToPage("./Index"));
        }

        private bool ConfigurationExists(int id)
        {
            return configRepository.GetConfigurationById(id) != null;
        }
    }
}
