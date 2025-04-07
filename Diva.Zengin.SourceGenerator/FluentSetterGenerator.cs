﻿using System;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Diva.Zengin.SourceGenerator;

[Generator(LanguageNames.CSharp)]
public class FluentSetterGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(static context =>
        {
            context.AddSource("FluentSetterAttribute.cs",
                """
                namespace Diva.Zengin;

                using System;

                [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
                internal sealed class FluentSetterAttribute : Attribute
                {
                }
                """);
        });

        var source = context.SyntaxProvider.ForAttributeWithMetadataName(
            "Diva.Zengin.FluentSetterAttribute",
            static (node, token) => true,
            static (context, token) =>
                context);

        context.RegisterSourceOutput(source, Emit);
    }

    private static void Emit(SourceProductionContext context, GeneratorAttributeSyntaxContext source)
    {
        var typeSymbol = (INamedTypeSymbol)source.TargetSymbol;
        var typeNode = (TypeDeclarationSyntax)source.TargetNode;

        var ns = typeSymbol.ContainingNamespace.IsGlobalNamespace
            ? ""
            : $"namespace {typeSymbol.ContainingNamespace};";

        var fullType = typeSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)
            .Replace("global::", "")
            .Replace("<", "_")
            .Replace(">", "_");

        var publicProperties = typeSymbol.GetMembers()
            .OfType<IPropertySymbol>()
            .Where(p => p.DeclaredAccessibility == Accessibility.Public && p is { IsStatic: false, SetMethod: not null });

        var methods = publicProperties.Select(p =>
        {
            var propertyName = p.Name;
            var propertyType = p.Type.ToDisplayString();
            var xmlComment = p.GetDocumentationCommentXml();
            var formattedXmlComment = string.Empty;
            if (xmlComment != null)
            {
                var memberTagContent = System.Text.RegularExpressions.Regex.Match(xmlComment, @"<member[^>]*>(.*?)</member>", System.Text.RegularExpressions.RegexOptions.Singleline).Groups[1].Value;
                formattedXmlComment = string.Join("\n", memberTagContent
                    .Split('\n')
                    .Select(line => line.Trim())
                    .Where(line => !string.IsNullOrWhiteSpace(line))
                    .Select(line => $"/// {line}"));
            }
            
            var method = $$"""
                           {{formattedXmlComment}}
                           public {{typeSymbol.Name}} Set{{propertyName}}({{propertyType}} value)
                           {
                               this.{{propertyName}} = value;
                               return this;
                           }
                           """;

            if (propertyType is not ("int" or "int?" or "decimal" or "decimal?"))
                return method;

            var parseMethod = propertyType switch
            {
                "int" => "int.Parse",
                "int?" => "int.TryParse",
                "decimal" => "decimal.Parse",
                "decimal?" => "decimal.TryParse",
                _ => throw new InvalidOperationException()
            };

            var parseCode = propertyType.EndsWith("?")
                ? $$"""
                    if ({{parseMethod}}(value, out var parsedValue))
                        {
                            this.{{propertyName}} = parsedValue;
                        }
                    """
                : $"this.{propertyName} = {parseMethod}(value);";

            method += $$"""
                        
                        {{formattedXmlComment}}
                        public {{typeSymbol.Name}} Set{{propertyName}}(string value)
                        {
                            {{parseCode}}
                            return this;
                        }
                        """;

            return method;
        }).ToList();

        var indentedMethods = string.Join("\n\n", methods)
            .Split('\n')
            .Select(line => string.IsNullOrWhiteSpace(line) ? line : "    " + line)
            .Aggregate((current, next) => current + "\n" + next);

        var code = $$"""
                     // <auto-generated/>
                     #nullable enable
                     #pragma warning disable CS8600
                     #pragma warning disable CS8601
                     #pragma warning disable CS8602
                     #pragma warning disable CS8603
                     #pragma warning disable CS8604

                     {{ns}}

                     partial class {{typeSymbol.Name}}
                     {
                     {{indentedMethods}}
                     }
                     """;

        context.AddSource($"{fullType}.FluentSetter.g.cs", code);
    }
}