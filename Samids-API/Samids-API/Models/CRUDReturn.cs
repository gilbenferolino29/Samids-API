using Microsoft.OpenApi.Any;

namespace Samids_API.Models
{
    public class CRUDReturn
    {
        public bool success { get; set; }
        public dynamic data { get; set; }
    }
}
