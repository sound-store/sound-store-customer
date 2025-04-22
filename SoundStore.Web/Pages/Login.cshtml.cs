using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SoundStore.Commons;
using SoundStore.Commons.Responses;
using System.Text.Json;

namespace SoundStore.Web.Pages
{
    public class LoginModel(ILogger<LoginModel> logger,
        IHttpClientFactory httpClientFactory,
        IHttpContextAccessor httpContextAccessor) : PageModel
    {
        private readonly ILogger<LoginModel> _logger = logger;
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        [BindProperty]
        public LoginRequest LoginRequest { get; init; } = null!;

        [BindProperty]
        public bool RememberMe { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.PostAsJsonAsync(SoundStoreEndpoints.LoginEndpoint, LoginRequest);
                var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                
                if (!response.IsSuccessStatusCode)
                {
                    var errorStream = await response.Content.ReadAsStreamAsync();
                    var errorResponse = await JsonSerializer.DeserializeAsync<ErrorResponse>(errorStream, jsonOptions);
                    ModelState.AddModelError(string.Empty, errorResponse!.Message ?? "Error");
                    return Page();
                }
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var apiResponse = await JsonSerializer
                    .DeserializeAsync<ApiResponse<LoginResponse>>(responseStream, jsonOptions);

                var user = apiResponse!.Value;
                _httpContextAccessor.HttpContext?.Session?.SetString("AccessToken", user!.Token!);
                return RedirectToPage("/Index");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error occurred during login");
                ModelState.AddModelError(string.Empty, "An error occurred. Please try again.");
                return Page();
            }
        }
    }
}
