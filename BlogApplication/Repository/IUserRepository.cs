using BlogApplication.Models;

namespace BlogApplication.Repository
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(int id);
        Task<List<User>> GetAllAsync();
    }

}
