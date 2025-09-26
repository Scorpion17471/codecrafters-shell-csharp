using System.Data;
using System.Net;
using System.Net.Sockets;

// Uncomment this line to pass the first stage
Console.Write("$ ");

var command = string.Empty;

do
{
    // Wait for user input
    command = Console.ReadLine();

    // Return invalid command message
    Console.WriteLine($"{command}: command not found");
} while (command != null);