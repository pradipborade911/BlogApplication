using AutoMapper;
using BlogApplication.DTOs;
using BlogApplication.Models;
using BlogApplication.Repository;

namespace BlogApplication.Service.impl
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public PostService(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public async Task<PostDetailsDTO> GetPostByIdAsync(int id)
        {
            var post = await _postRepository.GetByIdAsync(id);
            return _mapper.Map<PostDetailsDTO>(post);
        }

        public async Task<IEnumerable<PostDetailsDTO>> GetAllPostsAsync()
        {
            var posts = await _postRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PostDetailsDTO>>(posts);
        }

        public async Task CreatePostAsync(PostRequestDTO postDto)
        {
            var post = _mapper.Map<Post>(postDto);
            await _postRepository.AddAsync(post);
        }

        public async Task UpdatePostAsync(int id, PostRequestDTO postDto)
        {
            var post = await _postRepository.GetByIdAsync(id);
            if (post != null)
            {
                _mapper.Map(postDto, post);
                await _postRepository.UpdateAsync(post);
            }
        }

        public async Task DeletePostAsync(int id)
        {
            await _postRepository.DeleteAsync(id);
        }
    }

}
