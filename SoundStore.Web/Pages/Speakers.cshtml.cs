using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SoundStore.Commons;
using SoundStore.Commons.Responses;
using System.Text.Json;

namespace SoundStore.Web.Pages
{
    public class SpeakerModel(IHttpClientFactory httpClientFactory,
        ILogger<SpeakerModel> logger) : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
        private readonly ILogger _logger = logger;

        public PaginatedList<ProductResponse> Speakers { get; set; } = new PaginatedList<ProductResponse>();

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public async Task<IActionResult> OnGetAsync(int categoryId, int pageNumber = 1)
        {
            CurrentPage = pageNumber;
            try
            {
                var client = _httpClientFactory.CreateClient();
                var uri = SoundStoreEndpoints.GetProductByCategory +
                    $"{categoryId}/" + $"{PaginationConstants.PageNumberPathParam}/{pageNumber}/"
                    + $"{PaginationConstants.PageSizePathParam}/{PaginationConstants.PageSize}";

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var response = await client.GetAsync(uri);
                // TODO: Handle error when loading speakers failed
                if (!response.IsSuccessStatusCode)
                {
                    var errorStream = await response.Content.ReadAsStreamAsync();
                    var apiResponse = await JsonSerializer.DeserializeAsync<ErrorResponse>(errorStream, options);
                    ModelState.AddModelError(string.Empty, apiResponse!.Message);
                }

                var stream = await response.Content.ReadAsStreamAsync();
                var result = await JsonSerializer.DeserializeAsync<ApiResponse<PaginatedList<ProductResponse>>>(stream, options);
                Speakers = result!.Value!;

                return Page();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while fetching the data!");
                throw;
            }

        }
    }
}
