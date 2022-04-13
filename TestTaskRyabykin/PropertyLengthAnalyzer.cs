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
    public class PropertyLengthAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "PropertyLengthAnalyzer";

        private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Resources.PropertyLengthAnalyzerTitle), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString MessageFormat = new LocalizableResourceString(nameof(Resources.PropertyLengthAnalyzerFormat), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString Description = new LocalizableResourceString(nameof(Resources.PropertyLengthAnalyzerDescription), Resources.ResourceManager, typeof(Resources));
        private const string Category = "Naming";

        private static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, isEnabledByDefault: true, description: Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.EnableConcurrentExecution();

            context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.PropertyDeclaration);
        }

        private void AnalyzeNode(SyntaxNodeAnalysisContext context)
        {
            const int F = 10;
            var propertyDeclaration = (PropertyDeclarationSyntax)context.Node;
            if (propertyDeclaration.Identifier.Text.Length > F)
            {
                context.ReportDiagnostic(Diagnostic.Create(Rule, propertyDeclaration.Identifier.GetLocation(), propertyDeclaration.Identifier.Text));
            }
        }
    }
}
