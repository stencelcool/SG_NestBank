namespace SecondExample_ConsoleApp.Vehicles;

public class Car : Vehicle
{
    public string? Model { get; set; }

    public Car(string name) : base(name) { }
}