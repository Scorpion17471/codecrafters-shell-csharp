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

            // Clean out any doubled quotes (treated as nonexistent anyway)
            input = input.Replace("''", "").Replace("\"\"", "");

            // Setup for argument parsing
            List<String> output = [];
            int i = 0;
            StringBuilder arg = new();

            // Parse through input
            while (i < input.Length)
            {
                // CASES: Escaped Character, Double Quote, Single Quote, Normal Character, Whitespace
                char c = input[i];

                // Escape Character - Add next character as normal character
                if (c == '\\' && i < input.Length - 1)
                {
                    arg.Append(input[i + 1]);
                    i += 2;
                    continue;
                }

                // Single Quote - Read until next single quote to complete arg (no escaping)
                if (c == '\'')
                {
                    if (i < input.Length - 1)
                    {
                        // Continue through, parsing each char
                        i++;
                        while (i < input.Length && input[i] != '\'')
                        {
                            arg.Append(input[i]);
                            i++;
                        }
                        if (arg.ToString().Trim().Length > 0)
                        {
                            output.Add(arg.ToString());
                            arg.Clear();
                        }
                    }
                }
                // Double Quote - Read until next double quote to complete arg
                else if (c == '\"')
                {
                    if (i < input.Length - 1)
                    {
                        while (i < input.Length && input[i] != '\"')
                        {
                            if (input[i] == '\\' && i < input.Length - 1 &&
                                (input[i + 1] == '\"' || input[i + 1] == '\\'))
                            {
                                i++;
                            }
                            arg.Append(input[i]);
                            i++;
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