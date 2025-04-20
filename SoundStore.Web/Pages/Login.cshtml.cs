using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace SoundStore.Web.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "Email is required!", AllowEmptyStrings = false)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;

        [BindProperty]
        [Required(ErrorMessage = "Password is required!", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [BindProperty]
        public bool RememberMe { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                // TODO: Store the current values so it cannot lose when client's errors occured
                return Page();
            }

            // TODO: Add authentication logic here

            return RedirectToPage("/Index");
        }
    }
}
