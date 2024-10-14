using BlogApplication.Models;

namespace BlogApplication.DTOs
{
    public class PaginatedPostListDTO
    {
        public IEnumerable<PostSummaryDTO> Posts { get; set; } = new List<PostSummaryDTO>();
        public int TotalItems { get; set; } = 0;
        public int PageNumber { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public string SearchQuery { get; set; } = string.Empty;
        public IEnumerable<Tag> AvailableTags { get; set; } = new List<Tag>();
        public List<User> AvailableAuthors { get; set; } = new List<User>();
        public List<long> SelectedTags { get; set; } = null;
        public List<long> SelectedAuthors { get; set; } = null;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
