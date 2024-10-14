using BlogApplication.Models;

namespace BlogApplication.Repository
{
    public interface ITagRepository
    {
        Task<Tag> GetByIdAsync(int id);

        Task<Tag> GetByNameAsync(string name);

        Task<IEnumerable<Tag>> GetAllAsync();
    }
}
