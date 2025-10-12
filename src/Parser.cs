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
            int i = 0;
            StringBuilder arg = new();

            // Parse through input
            while (i < input.Length)
            {
                // CASES: Single Quote, Normal Character, Whitespace
                char c = input[i];

                // Single Quote - Read until next single quote to complete arg
                if (c == '\'')
                {
                    if (i < input.Length - 1)
                    {
                        // Check for two single quotes in a row (empty arg)
                        if (input[i + 1] == '\'') { i += 2; continue; }

                        i++;
                        while (i < input.Length && input[i] != '\'')
                        {
                            arg.Append(input[i]);
                            i++;
                        }
                        if (i < input.Length - 2 && input[i + 1] == '\'')
                        {
                            i += 2; // Skip closing quote and opening quote of next arg
                            while (i < input.Length && input[i] != '\'')
                            {
                                arg.Append(input[i]);
                                i++;
                            }
                        }
                        if (arg.ToString().Trim().Length > 0)
                        {
                            output.Add(arg.ToString());
                            arg.Clear();
                        }
                    }
                }
                // White Space - Skip/End
                else if (char.IsWhiteSpace(c))
                {
                    if (arg.ToString().Trim().Length > 0)
                    {
                        output.Add(arg.ToString().Trim());
                        arg.Clear();
                    }
                }
                else
                {
                    // Normal Character
                    arg.Append(c);
                }
                i++;
            }
            // Add last arg if exists
            if (arg.ToString().Trim().Length > 0)
            {
                output.Add(arg.ToString().Trim());
            }

            return output;
        }
    }
}
