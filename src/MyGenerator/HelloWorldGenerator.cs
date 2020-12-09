using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace MyGenerator {
    [Generator]
    public class HelloWorldGenerator : ISourceGenerator {
        public void Execute(GeneratorExecutionContext context) {
            var sourceBuilder = new StringBuilder();
            sourceBuilder.AppendLine("using System;");
            sourceBuilder.AppendLine("namespace HelloWorldGenerated {");
            sourceBuilder.AppendLine("  public static class HelloWorld {");
            sourceBuilder.AppendLine("    public static void SayHello() {");
            sourceBuilder.AppendLine(@"      Console.WriteLine(""Hello from generated code!"");");

            var syntaxTrees = context.Compilation.SyntaxTrees;
            foreach (SyntaxTree tree in syntaxTrees) {
                sourceBuilder.AppendLine($@"      Console.WriteLine(@"" - {tree.FilePath}"");");
            }
            sourceBuilder.AppendLine("    }");
            sourceBuilder.AppendLine("  }");
            sourceBuilder.AppendLine("}");

            context.AddSource("HelloWorldGenerator.g.cs", SourceText.From(sourceBuilder.ToString(), Encoding.UTF8));
        }

        public void Initialize(GeneratorInitializationContext context) {

        }
    }
}