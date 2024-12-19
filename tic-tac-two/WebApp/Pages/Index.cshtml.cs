using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages;

public class IndexModel(ILogger<IndexModel> logger) : PageModel
{
    private readonly ILogger<IndexModel> _logger = logger;

    [BindProperty]
    public string? UserName { get; set; }
    
    [BindProperty(SupportsGet = true)]
    public string? Error { get; set; }

    public void OnGet()
    {
    }
    public IActionResult OnPost()
    {
        UserName = UserName?.Trim();
        if (!string.IsNullOrWhiteSpace(UserName))
        {
            return RedirectToPage("./NewGame" , new { UserName });
        }
        Error = "User Name Required";
        return Page();
    }
}