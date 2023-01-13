using Samids_API.Dto;
using Samids_API.Models;

namespace Samids_API.Services.Interface
{
    public interface IAuthService
    {

        //Verification for registration
        CRUDReturn CheckEmail(User user, string email);
        CRUDReturn CheckStudentIfRegistered(Student user, int id);

        //Password Encryption
        CRUDReturn VerifyPasswordHash(byte[] passowrdSalt, byte[] passowrdHash, string password);
        void CreatePasswordHash(string password, out byte[] passwordSalt, out byte[] passwordHash);

        Task<CRUDReturn> ChangePassword(string newPassword, User user);

        //User Auth Functions
        Task<CRUDReturn> Login(UserDto login);
        Task<CRUDReturn> Register(UserRegisterDto request);
    }
}
