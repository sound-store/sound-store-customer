namespace SoundStore.Web.Helpers
{
    public static class SessionHelper
    {
        private const string _accessTokenKey = "AccessToken";

        public static string? GetAccessToken(IHttpContextAccessor httpContextAccessor)
            => httpContextAccessor.HttpContext?.Session?.GetString($"{_accessTokenKey}");
    }
}
