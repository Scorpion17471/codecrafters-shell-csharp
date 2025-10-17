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
                if (c == '\\' && i + 1 < input.Length)
                {
                    arg.Append(input[i + 1]);
                    i += 2;
                    continue;
                }

                // Double Quote - Read until next double quote to complete arg
                else if (c == '\"')
                {
                    if (i + 1 < input.Length)
                    {
                        // Continue through, parsing each char
                        i++;
                        while (i < input.Length && input[i] != '\"')
                        {
                            if ((input[i] == '\\') && (i + 1 < input.Length) &&
                                (input[i + 1] == '\"' || input[i + 1] == '\\'))
                            {
                                i++;
                            }
                            arg.Append(input[i]);
                            i++;
                        }
                    }
                }
                // Single Quote - Read until next single quote to complete arg (no escaping)
                else if (c == '\'')
                {
                    if (i + 1 < input.Length)
                    {
                        i++;
                        while (i < input.Length && input[i] != '\'')
                        {
                            arg.Append(input[i]);
                            i++;
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