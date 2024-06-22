using Microsoft.CodeAnalysis;
using SecondExample_SourceGenerator;

namespace ThirdExample_SourceGenerator.SyntaxReceivers
{
    public class MainSyntaxtReceiver : ISyntaxReceiver
    {
        public DefinitionAggregateSyntaxtReceiver Definitions { get; } = new ();
        public ImplementAggregateSyntaxtReceiver Implements { get; } = new ();

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            Definitions.OnVisitSyntaxNode(syntaxNode);
            Implements.OnVisitSyntaxNode(syntaxNode);
        }
    }
}