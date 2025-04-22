using Microsoft.AspNetCore.Mvc.RazorPages;
using SoundStore.Commons;
using SoundStore.Commons.Responses;
using System.Text.Json;

namespace SoundStore.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;

        /// <summary>
        /// "LOA MARSHALL" category
        /// </summary>
        private readonly int _speakerCategoryId = 1;

        /// <summary>
        /// "TAI NGHE MARSHALL" category
        /// </summary>
        private readonly int _headphonesCategoryId = 2;

        public List<ProductResponse> Headphones { get; set; } = [];

        public List<ProductResponse> Speakers { get; set; } = [];

        public IndexModel(ILogger<IndexModel> logger, HttpClient httpClient,
            IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient = httpClient;
            _httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// Get products
        /// </summary>
        /// <returns></returns>
        public async Task OnGet()
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                
                string speakerUri = SoundStoreEndpoints.GetProductByCategory + $"{_speakerCategoryId}/" +
                    $"{PaginationConstants.PageNumberPathParam}/{PaginationConstants.PageNumber}/" +
                    $"{PaginationConstants.PageSizePathParam}/{PaginationConstants.PageSize}";

                string headphoneUri = SoundStoreEndpoints.GetProductByCategory + $"{_headphonesCategoryId}/" +
                    $"{PaginationConstants.PageNumberPathParam}/{PaginationConstants.PageNumber}/" +
                    $"{PaginationConstants.PageSizePathParam}/{PaginationConstants.PageSize}";
                var jsonOptions = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };

                // Send both requests concurrently
                var speakerTask = client.GetAsync(speakerUri);
                var headphoneTask = client.GetAsync(headphoneUri);
                await Task.WhenAll(speakerTask, headphoneTask);

                // Speakers response
                if (speakerTask.Result.IsSuccessStatusCode)
                {
                    using var stream = await speakerTask.Result.Content.ReadAsStreamAsync();
                    var speakerResponse = await JsonSerializer
                        .DeserializeAsync<ApiResponse<PaginatedList<ProductResponse>>>(stream, jsonOptions);
                    if (speakerResponse?.IsSuccess == true)
                        Speakers = speakerResponse.Value!.Items.ToList();
                }

                // Headphones response
                if (headphoneTask.Result.IsSuccessStatusCode)
                {
                    using var stream = await headphoneTask.Result.Content.ReadAsStreamAsync();
                    var headphoneResponse = await JsonSerializer
                        .DeserializeAsync<ApiResponse<PaginatedList<ProductResponse>>>(stream, jsonOptions);
                    if (headphoneResponse?.IsSuccess == true)
                        Headphones = headphoneResponse.Value!.Items.ToList();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, "Exception occurred while fetching products.");
                throw;
            }
        }
    }
}
