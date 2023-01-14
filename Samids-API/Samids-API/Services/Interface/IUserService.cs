using Microsoft.OpenApi.Any;
using Samids_API.Dto;
using Samids_API.Models;

namespace Samids_API.Services.Interface
{
    public interface IUserService
    {
        Task<CRUDReturn> GetUsers();
        Task<CRUDReturn> GetById(int id);
        Task<CRUDReturn> UpdateUser(UserUpdateDto request);
        
        Task<CRUDReturn> DeleteUser(int id);
        

       


        
    }
}
