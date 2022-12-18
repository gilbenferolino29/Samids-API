using Samids_API.Models;

namespace Samids_API.Services
{
    public interface IUserService
    {
        User? Register(User newUser);
    }
}
