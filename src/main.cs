using System.Data;
using System.Net;
using System.Net.Sockets;

while (true)
{
    // Uncomment this line to pass the first stage
    Console.Write("$ ");

    // Wait for user input
    var command = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(command))
    {
        break;
    }

    // Return invalid command message
    Console.WriteLine($"{command}: command not found");
}