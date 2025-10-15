using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src
{
    public static class Parser
    {
        // Helper enum to track quoting state
        private enum State
        {
            None,
            Single,
            Double
        };
        // Parser that takes input and separates it into arguments, handling quotes and escapes
        public static List<String> InputParser(string input)
        {
            // Requirement: if input is null or empty, return list with empty string
            if (String.IsNullOrEmpty(input)) return [""];

            List<String> output = [];
            StringBuilder arg = new();
            State state = State.None;
            bool sawQuoteInToken = false;

            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];

                // Handle escape (backslash)
                if (c == '\\')
                {
                    if (i + 1 < input.Length)
                    {
                        // Take next char
                        arg.Append(input[i + 1]);
                        i++;
                        continue;
                    }
                    else
                    {
                        // Trailing backslash -> append it
                        arg.Append('\\');
                        continue;
                    }
                }
                // Single quote toggles only outside double quotes
                if (c == '\'')
                {
                    if (state == State.None) // Set flag to start single quoted token
                    {
                        state = State.Single;
                        sawQuoteInToken = true;
                        continue;
                    }
                    else if (state == State.Single)
                    {
                        state = State.None;
                        // do not flush token here — could be empty token or doubled single quotes
                        continue;
                    }
                    else // inside double quotes is normal char
                    {
                        arg.Append(c);
                        continue;
                    }
                }

                // Double quote toggles only outside single quotes
                if (c == '\"')
                {
                    if (state == State.None) // Set flag to start double quoted token
                    {
                        state = State.Double;
                        sawQuoteInToken = true;
                        continue;
                    }
                    else if (state == State.Double) // End of double quoted token, could be empty token or doubled double quotes
                    {
                        state = State.None;
                        continue;
                    }
                    else // inside single quotes is normal char
                    {
                        arg.Append(c);
                        continue;
                    }
                }

                // Whitespace separates arguments only when not inside any quotes
                if (char.IsWhiteSpace(c) && state == State.None)
                {
                    if (arg.Length > 0 || sawQuoteInToken)
                    {
                        output.Add(arg.ToString());
                        arg.Clear();
                        sawQuoteInToken = false;
                    }
                    // skip repeated whitespace
                    continue;
                }

                // Normal character
                arg.Append(c);

                // End of input — if there is a token or we encountered quotes for an empty token, add it
                if (arg.Length > 0 || sawQuoteInToken)
                {
                    output.Add(arg.ToString());
                }

                return output;
            }
        }
    }
}
