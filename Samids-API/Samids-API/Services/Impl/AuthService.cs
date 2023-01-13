using Microsoft.EntityFrameworkCore;
using Samids_API.Data;
using Samids_API.Dto;
using Samids_API.Models;
using Samids_API.Services.Interface;
using System.Security.Cryptography;

namespace Samids_API.Services.Impl
{
    public class AuthService : IAuthService
    {
        private readonly SamidsDataContext _context;

        public AuthService(SamidsDataContext context)
        {
            _context = context;
        }

        public void CreatePasswordHash(string password, out byte[] passwordSalt, out byte[] passwordHash)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<CRUDReturn> ChangePassword(string newPassword, User user)
        {
            using(var hmac = new HMACSHA512(user.PasswordSalt)) 
            {
                var oldPass = user.PasswordHash;
                var newPass = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(newPassword));
                if(newPass == oldPass)
                {
                    return new CRUDReturn { success = false, data = PasswordSame };
                }
                user.PasswordHash = newPass;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return new CRUDReturn { success = true, data = PasswordChanged };
            }
        
        }

        public CRUDReturn VerifyPasswordHash(byte[] passwordSalt, byte[] passwordHash, string password)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                if (hash == passwordSalt)
                {
                    return new CRUDReturn
                    {
                        success = hash == passwordHash,
                        data = OkPassword
                    };
                }
                return new CRUDReturn { success = false, data = NGPassword };


            }
        }

        public async Task<CRUDReturn> Login(UserDto login)
        {
            var user = await _context.Users.Include(s => s.Student).FirstOrDefaultAsync(u => u.Student.StudentNo == Convert.ToInt32(login.Username));

            if (user is null)
            {
                return new CRUDReturn { success = false, data = UserNotRegistered };
            }

            var check = VerifyPasswordHash(user.PasswordSalt, user.PasswordHash, login.Password);

            if (check.success is true)
            {
                return new CRUDReturn { success = true, data = user };
            }
            return new CRUDReturn { success = false, data = check.data };

        }

        public async Task<CRUDReturn> Register(UserRegisterDto request)
        {
            var users = await _context.Users.Include(u => u.Student).AsNoTracking().ToListAsync();
            var student = await _context.Students.FirstOrDefaultAsync(s => s.StudentNo == request.StudentNo);

            if (student is null)
            {
                return new CRUDReturn { success = false, data = UserNotFound };
            }

            foreach (var user in users)
            {
                var emailCheck = CheckEmail(user, request.Email);
                var studentCheck = CheckStudentIfRegistered(user.Student, request.StudentNo);

                if (studentCheck.success is not true)
                {
                    return new CRUDReturn
                    { success = false, data = studentCheck.data };
                }
                if (emailCheck.success is not true)
                {
                    return new CRUDReturn
                    { success = false, data = emailCheck.data };
                };
            }

            CreatePasswordHash(request.Password, out byte[] passwordSalt, out byte[] passwordHash);

            User newUser = new()
            {
                Student = student,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PasswordSalt = passwordSalt,
                PasswordHash = passwordHash,
                Type = request.Type,
                Deleted = false
            };


            _context.Users.Add(newUser);
            _context.SaveChanges();

            return new CRUDReturn
            { success = true, data = newUser };
        }

        //Verifications for Registration
        public CRUDReturn CheckEmail(User user, string email)
        {
            if (user.Email == email)
            {
                return new CRUDReturn
                { success = false, data = EmailAlreadyExists };
            }
            return new CRUDReturn
            { success = true, data = OkEmail };
        }

        public CRUDReturn CheckStudentIfRegistered(Student student, int id)
        {
            if (student.StudentNo == id)
            {
                return new CRUDReturn
                { success = false, data = StudentAlreadyRegistered };
            }
            return new CRUDReturn
            { success = true, data = OkStudent };
        }
    }
}
