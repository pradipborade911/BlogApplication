using BlogApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogApplication.Repository.impl
{
    public class TagRepository : ITagRepository
    {
        private readonly AppDbContext _dbContext;

        public TagRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Tag> GetByIdAsync(int id)
        {
            return await _dbContext.Tags.FindAsync(id);
        }

        public async Task<Tag> GetByNameAsync(string name)
        {
            return await _dbContext.Tags.FirstOrDefaultAsync(tag => tag.Name == name);
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            var t =  await _dbContext.Tags.ToListAsync();
            return t;
        }

    }

}

