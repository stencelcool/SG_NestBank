using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using System.Diagnostics;
using System.IO;

namespace FifthExample_Pipe_SourceGenerator;

[Generator]
public class ConstGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext initContext)
    {
        //if (!Debugger.IsAttached)
        //{
        //    Debugger.Launch();
        //}

        // get the additional text provider
        IncrementalValuesProvider<AdditionalText> additionalTexts = initContext.AdditionalTextsProvider.Where(static file => file.Path.EndsWith(".txt"));

        // apply a 1-to-1 transform on each text, extracting the path
        IncrementalValuesProvider<string> transformed = additionalTexts.Select(static (text, _) => Path.GetFileNameWithoutExtension(text.Path));

        // collect the paths into a batch
        IncrementalValueProvider<ImmutableArray<string>> collected = transformed.Collect();

        // take the file paths from the above batch and make some user visible syntax
        initContext.RegisterSourceOutput(collected, static (sourceProductionContext, files) =>
        {
            var names = string.Join(",", files);

            var code = $$"""
            namespace AdditionalFilesGenerated;

            public static class AdditionalTextList
            {
                public static void PrintTexts()
                {
                    System.Console.WriteLine("{{names}}");
                }
            }
            """;

            sourceProductionContext.AddSource("AdditionalFiles.g.cs", code);
        });
    }
}