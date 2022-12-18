using Samids_API.Models;

namespace Samids_API.Services
{
    public interface IUserService
    {
        Task<User?> Register(User newUser);
        Task<IEnumerable<User>> GetAll();
        Task<User?> GetById(int id);
        Task<User?> UpdateUser(User newUser);
        Task<User?> DeleteUser(int id);
    }
}
