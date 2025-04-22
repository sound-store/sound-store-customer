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
    }
}
