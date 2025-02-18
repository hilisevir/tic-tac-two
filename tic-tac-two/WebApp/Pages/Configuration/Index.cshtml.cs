using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Domain;

namespace WebApp.Pages_Configuration
{
    public class IndexModel(IConfigRepository configRepository) : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string? UserName { get; set; }
        

        public IList<Configuration> Configuration { get; set; } = new List<Configuration>();

        public Task<IActionResult> OnGetAsync()
        {
            var configDict = configRepository.GetConfigurationNames();
            foreach (var config in configDict)
            {
                var configuration = configRepository.GetConfigurationById(config.Key);
                Configuration.Add(configuration);
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
