using BlogApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogApplication.Repository.impl
{
    public class PostRepository : IPostRepository
    {
        private readonly AppDbContext _dbContext;

        public PostRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Post> GetByIdAsync(int id)
        {
            return await _dbContext.Posts.Include(p => p.Tags)
                .SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            return await _dbContext.Posts.Include(p => p.Tags)
                                         .Include(p => p.User)
                                         .Include(p => p.Comments)
                                         .ToListAsync();
        }

        public async Task AddAsync(Post post)
        {
            await _dbContext.Posts.AddAsync(post);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Post post)
        {
            _dbContext.Posts.Update(post);
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var post = new Post { Id = id };
            _dbContext.Posts.Attach(post);
            _dbContext.Posts.Remove(post);
            await _dbContext.SaveChangesAsync();

        }

        public IQueryable<Post> GetAllAsQueryable()
        {
            return _dbContext.Posts.Include(p => p.Tags)
                                   .Include(p => p.User)
                                   .AsQueryable();
        }

    }

}
