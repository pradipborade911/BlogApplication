using BlogApplication.DTOs;

namespace BlogApplication.Service
{
    public interface IPostService
    {
        Task<PostDetailsDTO> GetPostByIdAsync(int id);
        Task<PostRequestDTO> GetPostRequestDTOByIdAsync(int id);
        Task<IEnumerable<PostSummaryDTO>> GetAllPostsAsync();
        Task CreatePostAsync(PostRequestDTO postDto);
        Task UpdatePostAsync(int id, PostRequestDTO postDto);
        Task DeletePostAsync(int id);
        Task<PaginatedPostListDTO> GetFilteredPostsAsync(
            string searchQuery,
            List<long> tagIds,
            List<long> userIds,
            DateTime? startDate,
            DateTime? endDate,
            int pageNumber,
            int pageSize);
    }

}
