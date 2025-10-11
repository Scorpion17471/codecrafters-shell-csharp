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
                else if (!String.IsNullOrEmpty(input) && input.ToLower()[..4] == "echo")
                {
                    Console.Write(Commands.Echo(input) + '\n');
                }

                // Type Command
                else if (!String.IsNullOrEmpty(input) && input.ToLower()[..4] == "type")
                {
                    Console.Write(Commands.Type(input[5..]) + '\n');
                }

                // PWD Command
                else if (!String.IsNullOrEmpty(input) && input.ToLower()[..3] == "pwd")
                {
                    //This should be working
                    Console.Write(Commands.PWD());
                }

                // Unknown Command
                else
                {
                    // Try command
                    Commands.TestCommand(input);
                }
            }
        }
    }
}