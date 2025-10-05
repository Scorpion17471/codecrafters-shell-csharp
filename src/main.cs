using System.Data;
using System.Net;
using System.Net.Sockets;

while (true)
{
    // Uncomment this line to pass the first stage
    Console.Write("$ ");

    // Wait for user input
    String? input = Console.ReadLine()?.Trim();

    if (string.IsNullOrWhiteSpace(input) || input.ToLower() == "exit")
    {
        break;
    }

    // Return invalid command message
    Console.WriteLine($"{input}: command not found");
}