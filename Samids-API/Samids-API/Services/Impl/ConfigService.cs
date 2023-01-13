using Samids_API.Data;
using Samids_API.Models;
using Samids_API.Services.Interface;

namespace Samids_API.Services.Impl
{
    public class ConfigService : IConfigService
    {
        private readonly SamidsDataContext _context;

        public ConfigService(SamidsDataContext context)
        {
            _context = context;
        }
        public async Task<CRUDReturn> NewConfig(Config config)
        {
            foreach (Config row in _context.Configs)
            {
                _context.Configs.Remove(row);
            }
            _context.SaveChanges();
            await _context.Configs.AddAsync(config);
            _context.SaveChanges();

            return new CRUDReturn { success = true, data = config };
        }
    }
}
