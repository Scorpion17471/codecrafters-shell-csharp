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
            TextWriter originalOut = Console.Out;
            while (true)
            {
                Console.SetOut(originalOut);
                // Uncomment this line to pass the first stage
                Console.Write("$ ");

                // Wait for user input
                String? input = Console.ReadLine()?.Trim();

                List<String> arguments = Parser.InputParser(input ?? String.Empty);
                String command = arguments[0];
                arguments = arguments[1..];
                
                for (int j = 0; j < arguments.Count; j++)
                {
                    if (arguments[j] == "1>" || arguments[j] == ">")
                    {
                        if (j + 1 < arguments.Count)
                        {
                            String fileName = arguments[j + 1];
                            FileStream fileStream = new FileStream(fileName, FileMode.Append, FileAccess.Write);
                            StreamWriter streamWriter = new StreamWriter(fileStream)
                            {
                                AutoFlush = true
                            };
                            Console.SetOut(streamWriter);
                            arguments.RemoveRange(j, 2);
                            break;
                        }
                    }
                }

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
                    Console.Write(Commands.CD(arguments) + '\n');
                }

                // Unknown Command
                else
                {
                    // Try command
                    Console.Write(Commands.TestCommand(command, arguments) + '\n');
                }
            }
        }
    }
}