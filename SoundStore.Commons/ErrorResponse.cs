namespace SoundStore.Commons
{
    public class ErrorResponse
    {
        public bool IsSuccess { get; set; } = false;

        public string Message { get; set; } = string.Empty;
    }
}
