namespace BlogApplication.DTOs
{
    public class CommentDetailsDTO
    {
        public int Id { get; set; }
        public PostDetailsDTO PostDetailsDTO { get; set; }
        public UserDetailsDTO User { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
