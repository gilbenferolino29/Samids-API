using Samids_API.Data;
using Samids_API.Models;
using Samids_API.Services.Interfaces;

namespace Samids_API.Services
{
    public class UserService : IUserService
    {
        private readonly SamidsDataContext _context;

        public UserService(SamidsDataContext context)
        {
            _context = context;
        }
        public Task<User?> DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _context.Users.AsNoTracking().ToListAsync();
        }

        public async Task<User?> GetById(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user is null)
            {
                throw new InvalidOperationException("User doesn't exist");
            }

            return user;

        }

        public Task<User?> Register(User newUser)
        {
            throw new NotImplementedException();
        }

        public Task<User?> UpdateUser(User newUser)
        {
            throw new NotImplementedException();
        }
    }
}
