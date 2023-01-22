namespace Samids_API.Models
{
    sealed class Broker
    {

        public Server? HiveTest { get; set; }



        public string GetWebSocket()
        {
            return new String($"{HiveTest?.Url}:{HiveTest?.Port}");
        }


    }

    public class Server {
        public string Url { get; set; }
        public int Port { get; set; }
        public string Uname { get; set; }
        public string Pass { get; set; }
    }
}
