using Samids_API.Models;

namespace Samids_API.Services.Interfaces
{
    public interface IUserService
    {
        Task<User?> Register(User newUser);
        Task<IEnumerable<User>> GetUsers();
        Task<User?> GetById(int id);
        Task<User?> UpdateUser(User newUser);
        Task<User?> DeleteUser(int id);
    }
}
