using BlogApplication.Models;

namespace BlogApplication.DTOs
{
    public class PostSummaryDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Excerpt { get; set; }
        public DateTime? PublishedAt { get; set; }
        public List<Tag> Tags { get; set; }
        public UserDetailsDTO User { get; set; }
    }
}
