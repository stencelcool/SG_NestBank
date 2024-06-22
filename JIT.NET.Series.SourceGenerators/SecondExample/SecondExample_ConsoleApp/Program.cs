// See https://aka.ms/new-console-template for more information
using SecondExample_ConsoleApp;

Console.WriteLine("Hello, It's you favorite Console App");

Test.P();

// this class is defined here to show how code analyzer presents nodes etc.
public class Person2
{
    public string? Name { get; set; }

    public string? FavoriteVehicle { get; set; }
}