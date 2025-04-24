namespace SoundStore.Commons
{
    public class SoundStoreEndpoints
    {
        /// <summary>
        /// Development base url for all endpoints
        /// </summary>
        private const string _devBaseUrl = " https://localhost:7094/api/sound-store/";
        
        /// <summary>
        /// Sample endpoint: https://localhost:7094/api/sound-store/products/category/2/pageNumber/1/pageSize/10
        /// </summary>
        public const string GetProductByCategory = _devBaseUrl + $"products/category/";

        /// <summary>
        /// Sample endpoint: https://localhost:7094/api/sound-store/users/login
        /// </summary>
        public const string LoginEndpoint = _devBaseUrl + "users/login";

        /// <summary>
        /// Get categories
        /// </summary>
        public const string GetCategories = _devBaseUrl + "categories";

        /// <summary>
        /// Get product by id
        /// Sample endpoint: https://localhost:7094/api/sound-store/products/100
        /// </summary>
        public const string GetProductById = _devBaseUrl + "products/";
    }
}
