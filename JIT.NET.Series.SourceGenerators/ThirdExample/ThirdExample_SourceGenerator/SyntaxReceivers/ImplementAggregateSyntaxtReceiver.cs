using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;
using ThirdExample_SourceGenerator.Helpers;

namespace ThirdExample_SourceGenerator.SyntaxReceivers
{
    public class ImplementAggregateSyntaxtReceiver : ISyntaxReceiver
    {
        public List<ImplementAggregatePointer> AggregatedPointers = new ();

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is not AttributeSyntax { Name: IdentifierNameSyntax { Identifier.Text: "Implement" } } attr)
            {
                return;
            }

            var target = (attr.ArgumentList.Arguments.SingleOrDefault().Expression as LiteralExpressionSyntax).Token.ValueText;
            var method = attr.GetNodeParent<MethodDeclarationSyntax>();
            var classRepresentation = attr.GetNodeParent<ClassDeclarationSyntax>();

            AggregatedPointers.Add(new ImplementAggregatePointer(target, method, classRepresentation));
        }
    }
}