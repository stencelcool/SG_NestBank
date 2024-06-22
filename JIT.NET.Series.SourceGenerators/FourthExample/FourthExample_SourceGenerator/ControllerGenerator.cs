using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.IO;
using System.Linq;
using System.Text;

namespace FourthExample_SourceGenerator
{
    [Generator]
    public class ControllerGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            var syntaxTrees = context.Compilation.SyntaxTrees;
            var handlers = syntaxTrees.Where(st => st.GetText().ToString().Contains("[Http"));

            foreach (var handler in handlers)
            {
                var usingDirectives = handler.GetRoot().DescendantNodes().OfType<UsingDirectiveSyntax>();
                var usingDirectivesAsText = string.Join("\r\n", usingDirectives);
                var sourceBuilder = new StringBuilder(usingDirectivesAsText);

                var classDeclarationSyntax = handler.GetRoot().DescendantNodes().OfType<ClassDeclarationSyntax>().First();

                var className = classDeclarationSyntax.Identifier.ToString();
                var generatedClassName = $"{className}Controller";
                var splitClass = classDeclarationSyntax.ToString().Split(new[] { '{' }, 2);

                sourceBuilder.Append($@"
namespace GeneratedController
{{
    [ApiController]
    public class {generatedClassName} : ControllerBase
    {{
");
                sourceBuilder.AppendLine(splitClass[1].Replace(className, generatedClassName));
                sourceBuilder.AppendLine("}");

                File.WriteAllText($@"C:\Users\jstencel\Repositories\SourceGeneratorsExamples-main\JIT.NET.Series.SourceGenerators\SecondExample\SecondExample_SourceGenerator\LoggedInfo\webApiTesttxt.txt", sourceBuilder.ToString());


                context.AddSource("Test2.g.cs", SourceText.From(sourceBuilder.ToString(), Encoding.UTF8));
            }
        }

        public void Initialize(GeneratorInitializationContext context)
        { }
    }
}