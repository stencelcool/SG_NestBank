// See https://aka.ms/new-console-template for more information
using System.Reflection;

//First approach - reflection
//var classTypes = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsClass && t.IsPublic).ToList();

//if (classTypes is null)
//{
//    return;
//}

//foreach (var type in classTypes)
//{
//    Console.WriteLine(type.FullName);
//}

//Second approach
if (ClassListGenerator.ClassNames.Names is null)
{
    return;
}

foreach (var type in ClassListGenerator.ClassNames.Names)
{
    Console.WriteLine(type);
}