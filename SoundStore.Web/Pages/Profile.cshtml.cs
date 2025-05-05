using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SoundStore.Commons;
using SoundStore.Commons.Responses;
using SoundStore.Web.Helpers;
using System.Net.Http.Headers;
using System.Text.Json;

namespace SoundStore.Web.Pages
{
    public class ProfileModel(IHttpClientFactory httpClientFactory,
        ILogger<ProfileModel> logger,
        IHttpContextAccessor httpContextAccessor) : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
        private readonly ILogger<ProfileModel> _logger = logger;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public LoginResponse User { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                var userToken = SessionHelper.GetAccessToken(_httpContextAccessor);
                if (string.IsNullOrEmpty(userToken)) return RedirectToPage("/Index");

                var client = _httpClientFactory.CreateClient();
                var uri = SoundStoreEndpoints.GetMe;

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
                var response = await client.GetAsync(uri);
                var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                if (!response.IsSuccessStatusCode)
                {

                }
                var stream = await response.Content.ReadAsStreamAsync();
                var apiResponse = await JsonSerializer.DeserializeAsync<ApiResponse<LoginResponse>>(stream, jsonOptions);
                User = apiResponse!.Value!;
                
                return Page();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while fetching user's data!");
                ModelState.AddModelError(string.Empty, "Error");
                return RedirectToPage("/Index");
            }
        }

        public IActionResult OnPostLogoutAsync()
        {
            try
            {
                // Clear session data
                _httpContextAccessor.HttpContext?.Session.Clear();
                return RedirectToPage("/Index");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error occurred during logout.");
                ModelState.AddModelError(string.Empty, "An error occurred while logging out.");
                return Page();
            }
        }
    }
}
