using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace SoundStore.Web.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public UserRegistration Input { get; set; } = new();

        public void OnGet() { }

        public IActionResult OnPost()
        {
            // TODO: Store current values to reload when client's errors occurred
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //if (Input.Password != Input.ConfirmPassword)
            //{
            //    ModelState.AddModelError("Input.ConfirmPassword", "Passwords do not match.");
            //    return Page();
            //}

            // TODO: Register logic here
            return RedirectToPage("/Login");
        }
    }

    public class UserRegistration
    {
        [Required(ErrorMessage = "First name is required!", AllowEmptyStrings = false)]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required!", AllowEmptyStrings = false)]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Address is required!", AllowEmptyStrings = false)]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Date of birth is required!")]
        public DateOnly? DateOfBirth { get; set; }

        [Required(ErrorMessage = "Phone number is required!", AllowEmptyStrings = false)]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits.")]
        public string? PhoneNumber { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email is required!", AllowEmptyStrings = false)]
        public string? Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required!", AllowEmptyStrings = false)]
        [Length(8, 25, ErrorMessage = "Password must be between 8 and 25 characters long.")]
        [RegularExpression(@"^(?=.*[^a-zA-Z0-9]).+$",
            ErrorMessage = "Password must contain at least one special character.")]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please enter the password again!", AllowEmptyStrings = false)]
        [Length(8, 25, ErrorMessage = "Password must be between 8 and 25 characters long.")]
        public string? ConfirmPassword { get; set; }
    }
}
