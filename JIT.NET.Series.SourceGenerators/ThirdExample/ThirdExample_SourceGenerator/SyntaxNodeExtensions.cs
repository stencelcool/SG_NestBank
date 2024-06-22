using Microsoft.CodeAnalysis;
using System;

namespace ThirdExample_SourceGenerator
{
    public static class SyntaxNodeExtensions
    {
        public static T GetNodeParent<T>(this SyntaxNode node)
        {
            var parent = node.Parent;

            while (parent is not null)
            {
                if (parent is T t)
                {
                    return t;
                }

                parent = parent.Parent;
            }

            throw new Exception();
        }
    }
}