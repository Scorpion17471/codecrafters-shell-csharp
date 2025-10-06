using System.Data;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;

namespace src
{
    public class MainClass
    {
        public static int Main(string[] args)
        {
            while (true)
            {
                // Uncomment this line to pass the first stage
                Console.Write("$ ");

                // Wait for user input
                String? input = Console.ReadLine()?.Trim();

                // Check for exit command (Exit - Quits shell with given exit code)
                if (!String.IsNullOrEmpty(input) && input.ToLower()[..4] == "exit")
                {
                    return Convert.ToInt32(input[5..]);
                }

                // Echo Command
                else if (!String.IsNullOrEmpty(input) && input.ToLower().ToLower()[..4] == "echo")
                {
                    Console.Write(Commands.Echo(input));
                }
                // Invalid command
                else
                {
                    // Return invalid command message
                    Console.WriteLine($"{input}: command not found");
                }
            }
        }
    }
}