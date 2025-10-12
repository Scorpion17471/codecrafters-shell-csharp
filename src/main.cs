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
                String command = input?.Split(' ', 2)[0] ?? "";

                // Check for exit command (Exit - Quits shell with given exit code)
                if (!String.IsNullOrEmpty(input) && command == "exit")
                {
                    return Convert.ToInt32(input.Split(' ', 2)[1]);
                }

                // Echo Command
                else if (!String.IsNullOrEmpty(input) && command == "echo")
                {
                    Console.Write(Commands.Echo(input) + '\n');
                }

                // Type Command
                else if (!String.IsNullOrEmpty(input) && command == "type")
                {
                    Console.Write(Commands.Type(input.Split(' ', 2)[1]) + '\n');
                }

                // PWD Command
                else if (!String.IsNullOrEmpty(input) && command == "pwd")
                {
                    Console.Write(Commands.PWD() + '\n');
                }

                // CD Command
                else if (!String.IsNullOrEmpty(input) && command == "cd")
                {
                    Commands.CD(input.Split(' ', 2)[1]);
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