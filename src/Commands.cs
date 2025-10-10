using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace src
{
    public class Commands
    {
        private static readonly List<string> commands = [
            "echo",
            "exit",
            "type"
        ];
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
            string[] paths = Environment.GetEnvironmentVariable("PATH").Split(Path.PathSeparator);
            foreach (var path in paths)
            {
                string fullPath = Path.Combine(path, command);
                if (File.Exists(fullPath))
                {
                    // Check unix file permissions to see if executable
                    UnixFileMode info = File.GetUnixFileMode(fullPath);

                    if (info.ToString().Contains("execute", StringComparison.OrdinalIgnoreCase))
                    {
                        // Return if found and executable
                        return $"{command} is {fullPath}";
                    }
                }
            }

            // Default not found message
            return $"{command}: not found";
        }
    }
}
