using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using ThirdExample_SourceGenerator.Helpers;

namespace ThirdExample_SourceGenerator.SyntaxReceivers
{
    public class DefinitionAggregateSyntaxtReceiver : ISyntaxReceiver
    {
        public List<DefinitionAggregatePointer> AggregatedPointers { get; set; } = new();

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is not AttributeSyntax { Name: IdentifierNameSyntax { Identifier.Text: "Define" } } attr)
            {
                return;
            }

            var method = attr.GetNodeParent<MethodDeclarationSyntax>();
            var key = method.Identifier.Text;

            AggregatedPointers.Add(new DefinitionAggregatePointer(key, method));
        }
    }
}