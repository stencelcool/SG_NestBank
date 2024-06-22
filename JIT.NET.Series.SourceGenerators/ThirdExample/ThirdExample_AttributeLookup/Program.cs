// See https://aka.ms/new-console-template for more information
using ThirdExample_AttributeLookup;

var worker = new Worker();
worker.ExecuteJob();

public partial class Worker
{
    [Implement("WorkHard")]
    partial void DoTheJob();

    public void ExecuteJob() => DoTheJob();
}

public class Functions
{
    [Define]
    public static void WorkHard()
    {
        Console.WriteLine("Working hard as hell!");
    }
}