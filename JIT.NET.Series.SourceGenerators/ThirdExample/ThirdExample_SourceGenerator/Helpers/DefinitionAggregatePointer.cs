using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ThirdExample_SourceGenerator.Helpers
{
    public class DefinitionAggregatePointer
    {
        public string Key { get; set; }
        public MethodDeclarationSyntax Method { get; set; }

        public DefinitionAggregatePointer(string key, MethodDeclarationSyntax method)
        {
            Key = key;
            Method = method;
        }
    }
}