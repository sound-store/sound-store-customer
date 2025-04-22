using System.ComponentModel.DataAnnotations;

namespace SoundStore.Commons.Requests
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Email is required!", AllowEmptyStrings = false)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password is required!", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}
