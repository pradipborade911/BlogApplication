using AutoMapper;
using BlogApplication.DTOs;
using BlogApplication.Models;
using BlogApplication.Repository;
using Microsoft.EntityFrameworkCore;

namespace BlogApplication.Service.impl
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;

        public PostService(IPostRepository postRepository, IUserRepository userRepository, ITagRepository tagRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
            _tagRepository = tagRepository;
            _mapper = mapper;
        }

        public async Task<PostDetailsDTO> GetPostByIdAsync(int id)
        {
            var post = await _postRepository.GetByIdAsync(id);
            return _mapper.Map<PostDetailsDTO>(post);
        }
        public async Task<PostRequestDTO> GetPostRequestDTOByIdAsync(int id)
        {
            var post = await _postRepository.GetByIdAsync(id);
            var postRequestDTO = _mapper.Map<PostRequestDTO>(post);
            postRequestDTO.TagsList = string.Join(", ", post.Tags.Select(tag => tag.Name));
            return postRequestDTO;
        }
        public async Task<IEnumerable<PostSummaryDTO>> GetAllPostsAsync()
        {
            var posts = await _postRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PostSummaryDTO>>(posts);
        }

        public async Task CreatePostAsync(PostRequestDTO postDto)
        {
            var post = _mapper.Map<Post>(postDto);
            User user = await _userRepository.GetByIdAsync(1);
            post.User = user;
            post.CreatedAt = DateTime.Now;
            post.UpdatedAt = DateTime.Now;
            post.PublishedAt = DateTime.Now;
            post.IsPublished = true;

            var tags = new HashSet<Tag>();

            foreach (var tagName in postDto.TagsList.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
    .Select(s => s.Trim())
    .ToList())
            {
                var tag = await _tagRepository.GetByNameAsync(tagName) ?? new Tag { Name = tagName };
                tags.Add(tag);
            }

            post.Tags = tags;

            await _postRepository.AddAsync(post);
        }

        public async Task UpdatePostAsync(int id, PostRequestDTO postDto)
        {
            var post = await _postRepository.GetByIdAsync(id);
            if (post != null)
            {
                _mapper.Map(postDto, post);
                post.UpdatedAt = DateTime.Now;
                post.PublishedAt = DateTime.Now;

                var tags = new HashSet<Tag>();

                foreach (var tagName in postDto.TagsList.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
        .Select(s => s.Trim())
        .ToList())
                {
                    var tag = await _tagRepository.GetByNameAsync(tagName) ?? new Tag { Name = tagName };
                    tags.Add(tag);
                }

                post.Tags = tags;
                await _postRepository.UpdateAsync(post);
            }
        }

        public async Task DeletePostAsync(int id)
        {
            await _postRepository.DeleteAsync(id);
        }

        public async Task<PaginatedPostListDTO> GetFilteredPostsAsync(
            string searchQuery,
            List<long> tagIds,
            List<long> userIds,
            DateTime? startDate,
            DateTime? endDate,
            int pageNumber,
            int pageSize)
        {
            var postsQuery = _postRepository.GetAllAsQueryable();

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                postsQuery = postsQuery.Where(p =>
                    EF.Functions.Like(p.Content.ToLower(), $"%{searchQuery.ToLower()}%") ||
                    EF.Functions.Like(p.Title.ToLower(), $"%{searchQuery.ToLower()}%") ||
                    EF.Functions.Like(p.Excerpt.ToLower(), $"%{searchQuery.ToLower()}%") ||
                    EF.Functions.Like(p.User.Username.ToLower(), $"%{searchQuery.ToLower()}%") ||
                    p.Tags.Any(t => EF.Functions.Like(t.Name.ToLower(), $"%{searchQuery.ToLower()}%"))
                );


            }

            // Filter by date range
            if (startDate.HasValue)
            {
                postsQuery = postsQuery.Where(p => p.PublishedAt >= startDate.Value);
            }
            if (endDate.HasValue)
            {
                postsQuery = postsQuery.Where(p => p.PublishedAt <= endDate.Value);
            }

            if (tagIds != null && tagIds.Any())
            {
                postsQuery = postsQuery.Where(p => p.Tags.Any(t => tagIds.Contains(t.Id)));
            }

            if (userIds != null && userIds.Any())
            {
                postsQuery = postsQuery.Where(p => userIds.Contains(p.User.Id));
            }

            var totalItems = await postsQuery.CountAsync();
            var paginatedPosts = await postsQuery
                .OrderByDescending(p => p.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var postDtos = _mapper.Map<IEnumerable<PostSummaryDTO>>(paginatedPosts);

            var availableTags = await _tagRepository.GetAllAsync();
            var availableAuthors = await _userRepository.GetAllAsync();

            return new PaginatedPostListDTO
            {
                Posts = postDtos,
                AvailableTags = availableTags,
                AvailableAuthors = availableAuthors,

                TotalItems = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize,

                SearchQuery = searchQuery,
                SelectedTags = tagIds,
                SelectedAuthors = userIds,
                StartDate = startDate,
                EndDate = endDate
            };
        }

    }

}
