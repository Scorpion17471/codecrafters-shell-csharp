using System;
using System.Collections.Generic;
using System.Linq;
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
            if (commands.Contains(command))
            {
                return $"{command} is a shell builtin";
            }
            else
            {
                return $"{command}: not found";
            }
        }
    }
}
