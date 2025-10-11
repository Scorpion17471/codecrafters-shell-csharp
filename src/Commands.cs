using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace src
{
    public class Commands
    {
        // Readonly list of commands
        private static readonly List<string> commands = [
            "echo",
            "exit",
            "type"
        ];
        // IsExecutable - Check if file is executable
        private static bool IsExecutable(string path)
        {
            if (System.OperatingSystem.IsWindows())
            {
                #pragma warning disable CS8602 // Suppress possible null reference warning
                string[] executableExtensions = Environment.GetEnvironmentVariable("PATHEXT").Split(';');
                #pragma warning restore CS8602 // Restore possible null reference warning
                string fileExtension = Path.GetExtension(path);
                return executableExtensions.Contains(fileExtension, StringComparer.OrdinalIgnoreCase);
            }
            else
            {
                UnixFileMode info = File.GetUnixFileMode(path);
                return info.ToString().Contains("execute", StringComparison.OrdinalIgnoreCase);
            }
        }
        // Echo - Return everything in input string after "echo "
        public static string Echo(string input)
        {
            return input[5..];
        }
        // Type - Return type associated with command (built-in vs unrecognized)
        public static string Type(string command)
        {
            // Check if command is in list of built-in commands
            if (commands.Contains(command))
            {
                return $"{command} is a shell builtin";
            }

            // Get Paths and iterate through them to find command
            #pragma warning disable CS8602 // Suppress possible null reference warning
            string[] paths = Environment.GetEnvironmentVariable("PATH").Split(Path.PathSeparator);
            #pragma warning restore CS8602 // Restore possible null reference warning
            foreach (var path in paths)
            {
                string fullPath = Path.Combine(path, command);
                // If file exists and is executable, output its path
                if (File.Exists(fullPath) && IsExecutable(fullPath))
                {
                    return $"{command} is {fullPath}";
                }
            }

            // Default not found message
            return $"{command}: not found";
        }
        // TestCommand - Return invalid command message
        public static void TestCommand(string fullCommand)
        {
            // Split command into function and args (only first space)
            string[] args = fullCommand.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
            
            // Check for command in any path provided
            #pragma warning disable CS8602 // Suppress possible null reference warning
            foreach (var path in Environment.GetEnvironmentVariable("PATH").Split(Path.PathSeparator))
            #pragma warning restore CS8602 // Restore possible null reference warning
            {
                // Assemble full path to command
                string fullPath = Path.Combine(path, args[0]);
                // If file exists and is executable, run it with args
                if (File.Exists(fullPath) && IsExecutable(fullPath))
                {
                    // Start process with args if they exist
                    Process.Start(args[0], args[1]).WaitForExit();
                    return;
                }
            }

            // Return invalid command message if command not found in any path
            Console.WriteLine($"{args[0]}: command not found");
        }
    }
}
