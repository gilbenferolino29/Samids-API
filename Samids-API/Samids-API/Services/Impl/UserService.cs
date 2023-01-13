using Microsoft.OpenApi.Any;
using Samids_API.Data;
using Samids_API.Dto;
using Samids_API.Models;
using Samids_API.Services.Interface;
using System.Security.Cryptography;

namespace Samids_API.Services.Impl
{
    public class UserService : IUserService
    {
        private readonly SamidsDataContext _context;

        public UserService(SamidsDataContext context)
        {
            _context = context;
        }


        public async Task<CRUDReturn> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user is null)
            {
                return new CRUDReturn { success=false,data=UserNotFound};
            }
            _context.Users.Remove(user);
            _context.SaveChanges();
            return new CRUDReturn 
            { success = true, data = OkDelete };
        }

        public async Task<CRUDReturn> GetUsers()
        {
            return new CRUDReturn 
            { success = true, data = await _context.Users.AsNoTracking().ToListAsync() };
        }

        public async Task<CRUDReturn> GetById(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user is null)
            {
                return new CRUDReturn 
                { success=false,data=UserNotFound};
            }

            return new CRUDReturn 
            { success = true, data = user };

        }
        public Task<CRUDReturn> UpdateUser(UserUpdateDto request)
        {
            throw new NotImplementedException();
        }


    }
}
