using System.Data;
using System.Net;
using System.Net.Sockets;

while (true)
{
    // Uncomment this line to pass the first stage
    Console.Write("$ ");

    // Wait for user input
    String? input = Console.ReadLine()?.Trim();

    if (input.ToLower().ToLower()[..4] == "exit")
    {
        return Convert.ToInt32(input[5..]);
    }

    // Return invalid command message
    Console.WriteLine($"{input}: command not found");
}