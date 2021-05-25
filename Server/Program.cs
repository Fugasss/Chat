using System;
using System.Threading.Tasks;

namespace Server
{
    internal class Program
    {
        //TODO: send welcome packet from client with name
        private static void Main(string[] args)
        {
            var width = Console.WindowWidth;
            var height = Console.WindowHeight;

            Console.SetWindowSize(width / 2, height);

            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.DarkCyan;

            new Program().MainAsync().GetAwaiter().GetResult();
        }

        private async Task MainAsync()
        {
            var server = new Server(80);
            server.Start();

            await Task.Delay(-1);
        }
    }
}