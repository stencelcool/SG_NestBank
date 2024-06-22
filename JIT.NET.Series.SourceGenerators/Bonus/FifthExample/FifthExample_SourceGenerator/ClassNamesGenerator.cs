using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Immutable;
using System.Diagnostics;

namespace FifthExample_SourceGenerator;

[Generator]
public class ClassNamesGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        //Debugger.Launch();
        var provider = context.SyntaxProvider.CreateSyntaxProvider(
            predicate: (node, _) => node is ClassDeclarationSyntax,
            transform: (ctx, _) => (ClassDeclarationSyntax)ctx.Node
        ).Where(m => m is not null);

        var compilation = context.CompilationProvider.Combine(provider.Collect());

        context.RegisterSourceOutput(compilation, Execute);
    }

    private void Execute(SourceProductionContext context, (Compilation Left, ImmutableArray<ClassDeclarationSyntax> Right) tuple)
    {
        var (compilation, list) = tuple;

        var nameList = new List<string>();
        foreach( var item in list)
        {
            var symbol = compilation.GetSemanticModel(item.SyntaxTree).GetDeclaredSymbol(item) as INamedTypeSymbol;
            nameList.Add($"\"{symbol.ToDisplayString()}\"");
        }

        var names = string.Join(",\n    ", nameList);

        var code = $$"""
            namespace ClassListGenerator;

            public static class ClassNames
            {
                public static List<string> Names = new()
                {
                    {{ names }}
                };
            }
            """;

        context.AddSource("OurClassListNames.g.cs", code);
    }
}