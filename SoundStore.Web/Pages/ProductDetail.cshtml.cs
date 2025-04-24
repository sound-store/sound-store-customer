using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SoundStore.Commons;
using SoundStore.Commons.Responses;
using System.Text.Json;

namespace SoundStore.Web.Pages
{
    public class ProductDetailModel(ILogger<ProductDetailModel> logger,
        IHttpClientFactory httpClientFactory) : PageModel
    {
        private readonly ILogger<ProductDetailModel> _logger = logger;
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

        public ProductResponse Product { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync(long productId)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                string uri = SoundStoreEndpoints.GetProductById + $"{productId}";
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                var response = await client.GetAsync(uri);
                if (!response.IsSuccessStatusCode)
                {
                    // TODO: Handle error when fetching the data failed
                }
                var stream = await response.Content.ReadAsStreamAsync();
                var result = await JsonSerializer.DeserializeAsync<ApiResponse<ProductResponse>>(stream, options);
                Product = result!.Value!;
                
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
