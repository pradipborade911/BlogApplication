using BlogApplication.Models;

namespace BlogApplication.DTOs
{
    public class PostDetailsDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Excerpt { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? PublishedAt { get; set; }
        public bool IsPublished { get; set; }
        public ICollection<CommentDetailsDTO> Comments { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public UserDetailsDTO User { get; set; }
    }
}
