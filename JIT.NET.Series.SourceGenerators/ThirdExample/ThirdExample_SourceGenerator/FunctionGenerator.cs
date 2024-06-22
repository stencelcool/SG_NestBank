using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.IO;
using System.Linq;
using System.Text;
using ThirdExample_SourceGenerator.SyntaxReceivers;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace SecondExample_SourceGenerator
{
    [Generator]
    public class FunctionGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            var receiver = (MainSyntaxtReceiver)context.SyntaxReceiver;

            foreach (var implement in receiver.Implements.AggregatedPointers)
            {
                var definition = receiver.Definitions.AggregatedPointers.FirstOrDefault(p => p.Key == implement.Target);

                var output = implement.ClassRepresentation
                    .WithMembers(new(CreateMethodImplementation(implement.Method, definition.Method)))
                    .NormalizeWhitespace();

                File.WriteAllText($@"C:\Users\jstencel\Repositories\SourceGeneratorsExamples-main\JIT.NET.Series.SourceGenerators\SecondExample\SecondExample_SourceGenerator\LoggedInfo\classDeclarationSyntax.txt", output.ToFullString());

                context.AddSource($"{implement.ClassRepresentation.Identifier.Text}.g.cs", output.GetText(Encoding.UTF8));
            }

            //throw new Exception(receiver.Definitions.AggregatedPointers.First().Method.ToFullString().ReplaceLineEndings(""));
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new MainSyntaxtReceiver());
        }

        private MethodDeclarationSyntax CreateMethodImplementation(MethodDeclarationSyntax implement, MethodDeclarationSyntax definition)
        {
            return MethodDeclaration(implement.ReturnType, implement.Identifier)
                .WithModifiers(implement.Modifiers)
                .WithBody(definition.Body);
        }
    }
}