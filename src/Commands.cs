using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src
{
    public class Commands
    {
        // Echo - Return everything in input string after "echo "
        public static string Echo(string input)
        {
            return input[5..];
        }
    }
}
