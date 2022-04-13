using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;

namespace TestTaskRyabykin
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class FieldOrPropertyLengthAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "FieldOrPropertyLengthAnalyzer";

        private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Resources.FieldOrPropertyLengthAnalyzerTitle), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString MessageFormat = new LocalizableResourceString(nameof(Resources.FieldOrPropertyLengthAnalyzerFormat), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString Description = new LocalizableResourceString(nameof(Resources.FieldOrPropertyLengthAnalyzerDescription), Resources.ResourceManager, typeof(Resources));
        private const string Category = "Naming";

        private static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, isEnabledByDefault: true, description: Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.EnableConcurrentExecution();

            //context.RegisterSymbolAction(AnalyzeFieldNode, SymbolKind.Field);
            //context.RegisterSyntaxNodeAction(AnalyzeFieldNode, SyntaxKind.FieldDeclaration);
            context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.FieldDeclaration);
        }
        const int M = 10;
        /*
        private static void AnalyzeFieldNode(SymbolAnalysisContext context)
        {
            var propertyDeclaration = (IPropertySymbol)context.Symbol;
            if (propertyDeclaration.Name.Length > M)
            {
                context.ReportDiagnostic(Diagnostic.Create(Rule, context.Symbol.Locations[0], propertyDeclaration.Name));
            }
        }
        */
        private void AnalyzeNode(SyntaxNodeAnalysisContext context)
        {
            var fieldDeclaration = (FieldDeclarationSyntax)context.Node;
            int index = 0;
            foreach (var element in fieldDeclaration.Declaration.Variables)
            {
                if (element.Identifier.Text.Length > M)
                {
                    context.ReportDiagnostic(Diagnostic.Create(Rule, context.Node.GetLocation(), fieldDeclaration.Declaration.Variables.ElementAt(index).Identifier.Text));
                }
                ++index;
            }
        }

    }
}
