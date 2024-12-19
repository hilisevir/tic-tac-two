using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Domain;

namespace WebApp.Pages_Configuration
{
    public class DetailsModel(IConfigRepository configRepository) : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string? UserName { get; set; }
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
    }
}
