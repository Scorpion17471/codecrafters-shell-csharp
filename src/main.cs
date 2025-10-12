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

                // Setup for argument parsing
                String command = "";
                List<String> arguments = [];
                int slow = 0, fast = 0;
                char delimiter = ' ', old_delimiter = ' ';

                // Parse arguments, respecting single quotes
                while (fast < input.Length)
                {
                    if (input[fast] == delimiter && slow != fast)
                    {
                        if (slow == 0) command = input[slow..fast].Trim();
                        else arguments.Add(input[(slow + 1)..fast].Trim());
                        slow = fast;
                    }
                    if (input[fast] == '\'')
                    {
                        delimiter = delimiter == ' ' ? '\'' : ' ';
                    }
                    fast++;
                }

                // Check for exit command (Exit - Quits shell with given exit code)
                if (!String.IsNullOrEmpty(input) && command.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    return Convert.ToInt32(arguments[0]);
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
                    Commands.CD(input.Split(' ', 2)[1]);
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