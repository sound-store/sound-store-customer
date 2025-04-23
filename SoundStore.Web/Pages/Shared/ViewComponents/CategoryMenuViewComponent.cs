using Microsoft.AspNetCore.Mvc;
using SoundStore.Commons;
using SoundStore.Commons.Responses;
using System.Text.Json;

namespace SoundStore.Web.Pages.Shared.ViewComponents
{
    public class CategoryMenuViewComponent(IHttpClientFactory httpClientFactory) : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(SoundStoreEndpoints.GetCategories);

            if (!response.IsSuccessStatusCode)
                return View(new List<CategoryResponse>());

            var stream = await response.Content.ReadAsStreamAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var apiResponse = await JsonSerializer.DeserializeAsync<ApiResponse<List<CategoryResponse>>>(stream, options);
            return View(apiResponse!.Value);
        }
    }
}
