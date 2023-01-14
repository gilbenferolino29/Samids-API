using Samids_API.Models;

namespace Samids_API.Services.Interface
{
    public interface IConfigService
    {
        public Task<CRUDReturn> NewConfig(Config config);
        
    }
}
