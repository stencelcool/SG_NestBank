﻿namespace SecondExample_ConsoleApp.Vehicles;

public class Plane : Vehicle
{
    public int NumberOfWings { get; set; }

    public Plane(string name) : base(name) { }
}