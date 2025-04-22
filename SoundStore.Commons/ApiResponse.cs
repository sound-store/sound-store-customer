namespace SoundStore.Commons
{
    public record ApiResponse<T>
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; } = null!;

        public T? Value { get; set; }
    }
}
