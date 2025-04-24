namespace SoundStore.Commons.Responses
{
    public class RatingResponse
    {
        public string Username { get; set; } = null!;

        public int RatingPoint { get; set; }

        public string? Comment { get; set; }
    }
}
