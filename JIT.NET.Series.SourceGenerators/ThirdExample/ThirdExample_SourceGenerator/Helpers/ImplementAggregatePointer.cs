using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ThirdExample_SourceGenerator.Helpers
{
    public class ImplementAggregatePointer
    {
        public string Target { get; set; }
        public MethodDeclarationSyntax Method { get; set; }
        public ClassDeclarationSyntax ClassRepresentation { get; set; }

        public ImplementAggregatePointer(string target,
                                         MethodDeclarationSyntax method,
                                         ClassDeclarationSyntax classRepresentation)
        {
            Target = target;
            Method = method;
            ClassRepresentation = classRepresentation;
        }
    }
}