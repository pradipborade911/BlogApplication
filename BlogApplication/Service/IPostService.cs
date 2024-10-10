using BlogApplication.DTOs;

namespace BlogApplication.Service
{
    public interface IPostService
    {
        Task<PostDetailsDTO> GetPostByIdAsync(int id);
        Task<IEnumerable<PostDetailsDTO>> GetAllPostsAsync();
        Task CreatePostAsync(PostRequestDTO postDto);
        Task UpdatePostAsync(int id, PostRequestDTO postDto);
        Task DeletePostAsync(int id);
    }

}
