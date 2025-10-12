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

                List<String> arguments = Parser.InputParser(input ?? String.Empty);
                String command = arguments[0];
                arguments = arguments[1..];

                // DEBUGGING OUTPUT
                //Console.WriteLine($"Command: {command}");
                //foreach (var arg in arguments)
                //{
                //    Console.WriteLine($"Arg: {arg}");
                //}

                // Check for exit command (Exit - Quits shell with given exit code)
                if (!String.IsNullOrEmpty(input) && command.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    try
                    {
                        return Convert.ToInt32(arguments[0]);
                    }
                    catch
                    {
                        return 0;
                    }
                }

                // Echo Command
                else if (!String.IsNullOrEmpty(input) && command.Equals("echo", StringComparison.OrdinalIgnoreCase))
                {
                    Console.Write(Commands.Echo(arguments) + '\n');
                }

                // Type Command
                else if (!String.IsNullOrEmpty(input) && command.Equals("type", StringComparison.OrdinalIgnoreCase))
                {
                    Console.Write(Commands.Type(arguments) + '\n');
                }

                // PWD Command
                else if (!String.IsNullOrEmpty(input) && command.Equals("pwd", StringComparison.OrdinalIgnoreCase))
                {
                    Console.Write(Commands.PWD() + '\n');
                }

                // CD Command
                else if (!String.IsNullOrEmpty(input) && command.Equals("cd", StringComparison.OrdinalIgnoreCase))
                {
                    Commands.CD(arguments);
                }

                // Unknown Command
                else
                {
                    // Try command
                    Commands.TestCommand(command, arguments);
                }
            }
        }
    }
}