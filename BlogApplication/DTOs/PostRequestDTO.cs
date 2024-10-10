namespace BlogApplication.DTOs
{
    public class PostRequestDTO
    {
        public string Title { get; set; }
        public string Excerpt { get; set; }
        public string Content { get; set; }
        public bool IsPublished { get; set; }

        // For existing tags
        public List<int> TagIds { get; set; }

        // For new tags
        public List<string> NewTags { get; set; }
    }
}
