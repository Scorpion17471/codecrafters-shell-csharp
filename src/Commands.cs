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
        public static void Echo(string input)
        {
            Console.WriteLine(input[5..]);
            return;
        }
    }
}
