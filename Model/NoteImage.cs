namespace NotebookAppApi.Model
{
    public class NoteImage
    {
        public string Url { get; set; } = string.Empty;
        public string ThumbnailUrl { get; set; } = string.Empty;
        public long ImageSize { get; set; } = 0L;
    }
}