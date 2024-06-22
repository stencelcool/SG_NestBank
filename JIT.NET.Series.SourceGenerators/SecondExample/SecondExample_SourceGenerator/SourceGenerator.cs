using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace SecondExample_SourceGenerator
{
    [Generator]
    public class SourceGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            //Couple approaches of debugging Source Generators:

            // 1. Task Delay ;)
            //    this will give us a possibility to attach to a process VSCB Compiler
            //    Task.Delay(30_000).GetAwaiter().GetResult();

            //2. Logging into the file
            //   File.WriteAllText(@"C:\Users\Kuba Stencel\source\repos\JIT.NET.Series.SourceGenerators\SecondExample\SecondExample_SourceGenerator\LoggingInfo\log.txt", "test1");

            //3. Using Debugger
            //   Debugger.Launch();




            // Code generation goes here
            // FIRST EXAMPLE => Searching through Syntax Tree, the fastest approach, writing to the file

            //int i = 0;
            //foreach (var compilationSyntexTree in context.Compilation.SyntaxTrees)
            //{
            //    File.WriteAllText($@"C:\Users\jstencel\Repositories\SourceGeneratorsExamples-main\JIT.NET.Series.SourceGenerators\SecondExample\SecondExample_SourceGenerator\LoggedInfo\log{i++}.txt", compilationSyntexTree.GetText().ToString());
            //}



            // SECOND EXAMPLE => Searching through class declaration syntax types:

            //int i = 0;
            //foreach (var compilationSyntexTree in context.Compilation.SyntaxTrees)
            //{
            //    foreach (var classDeclarationSyntax in compilationSyntexTree
            //        .GetRoot()
            //        //.ChildNodes()
            //        .DescendantNodes()
            //        .OfType<ClassDeclarationSyntax>())
            //    {
            //        File.WriteAllText($@"C:\Users\jstencel\Repositories\SourceGeneratorsExamples-main\JIT.NET.Series.SourceGenerators\SecondExample\SecondExample_SourceGenerator\LoggedInfo\classDeclarationSyntax{i++}.txt", classDeclarationSyntax.GetText().ToString());
            //    }
            //}


            // THIRD EXAMPLE => Test File generation that can be used from Program.cs

            var output2 = CompilationUnit()
                .WithMembers(
                    SingletonList<MemberDeclarationSyntax>(
                        ClassDeclaration("Test")
                            .WithModifiers(
                                TokenList(
                                    new[]
                                    {
                                        Token(SyntaxKind.PublicKeyword),
                                        Token(SyntaxKind.StaticKeyword)
                                    }))
                            .WithMembers(
                                SingletonList<MemberDeclarationSyntax>(
                                    MethodDeclaration(
                                            PredefinedType(
                                                Token(SyntaxKind.VoidKeyword)),
                                            Identifier("P"))
                                        .WithModifiers(
                                            TokenList(
                                                new[]
                                                {
                                                    Token(SyntaxKind.PublicKeyword),
                                                    Token(SyntaxKind.StaticKeyword)
                                                }))
                                        .WithExpressionBody(
                                            ArrowExpressionClause(
                                                InvocationExpression(
                                                        MemberAccessExpression(
                                                            SyntaxKind.SimpleMemberAccessExpression,
                                                            IdentifierName("Console"),
                                                            IdentifierName("WriteLine")))
                                                    .WithArgumentList(
                                                        ArgumentList(
                                                            SingletonSeparatedList<ArgumentSyntax>(
                                                                Argument(
                                                                    LiteralExpression(
                                                                        SyntaxKind.StringLiteralExpression,
                                                                        Literal("Helllooooo"))))))))
                                        .WithSemicolonToken(
                                            Token(SyntaxKind.SemicolonToken))))))
                .NormalizeWhitespace();


            context.AddSource("generatedTest2Class.g.cs", output2.GetText(Encoding.UTF8).ToString());
        }

        public void Initialize(GeneratorInitializationContext context)
        {
//#if DEBUG
//            if (!Debugger.IsAttached)
//            {
//                Debugger.Launch();
//            }
//#endif

            // No initialization required for this one
            //context.RegisterForSyntaxNotifications(() => new MainSyntaxtReceiver());
        }
    }


    //Third approach to read files/types, in fact the proper approach

    public class MainSyntaxtReceiver : ISyntaxReceiver
    {
        public int Index { get; set; }
        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is ClassDeclarationSyntax)
            {
                File.WriteAllText($@"C:\Users\jstencel\Repositories\SourceGeneratorsExamples-main\JIT.NET.Series.SourceGenerators\SecondExample\SecondExample_SourceGenerator\LoggedInfo\classDeclarationSyntax{Index++}.txt", syntaxNode.GetText().ToString());
            }
        }
    }
}