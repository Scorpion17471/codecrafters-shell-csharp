using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src
{
    public static class Parser
    {
        public static List<String> InputParser(string input)
        {
            // If input is null or empty, return list with empty string
            if (String.IsNullOrEmpty(input)) return [""];

            // Setup for argument parsing
            List<String> output = [];
            int slow = 0, fast = 0;
            char delimiter = ' ';

            // Parse arguments, respecting single quotes
            while (fast < input.Length)
            {
                // if we have hit the end of a token, add it to output
                if (char.Equals(input[fast], delimiter))
                {
                    string arg = input[slow..fast].Trim();
                    if (arg.Length > 0) output.Add(arg);
                    slow = fast + 1;
                    // if we closed a quote token, reset delimiter to space
                    delimiter = delimiter == '\'' ? ' ' : delimiter;
                }
                // if we have hit a single quote, change delimiter and move slow pointer
                else if (char.Equals(input[fast], '\''))
                {
                    slow = fast + 1;
                    delimiter = '\'';
                }
                // move fast pointer forward
                fast++;
            }
            // Add last argument if there is one
            if (input[slow..fast].Trim().Length > 0) output.Add(input[slow..fast].Trim());

            return output;
        }
    }
}
