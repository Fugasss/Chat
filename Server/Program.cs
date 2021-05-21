using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        //TODO: send welcome packet from client with name
        static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

        async Task MainAsync()
        {
            var server = new Server(80);
            server.Start();

            await Task.Delay(-1);
        }
    }
}