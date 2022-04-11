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
    public class FieldLengthAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "FieldLengthAnalyzer";

        private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Resources.FieldLengthAnalyzerTitle), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString MessageFormat = new LocalizableResourceString(nameof(Resources.FieldLengthAnalyzerFormat), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString Description = new LocalizableResourceString(nameof(Resources.FieldLengthAnalyzerDescription), Resources.ResourceManager, typeof(Resources));
        private const string Category = "Naming";

        private static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, isEnabledByDefault: true, description: Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.EnableConcurrentExecution();

            context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.FieldDeclaration);
        }

        private void AnalyzeNode(SyntaxNodeAnalysisContext context)
        {
            const int F = 10;
            var fieldDeclaration = (FieldDeclarationSyntax)context.Node;
            int index = 0;
            foreach (var element in fieldDeclaration.Declaration.Variables)
            {
                if (element.Identifier.Text.Length > F)
                {
                    context.ReportDiagnostic(Diagnostic.Create(Rule, context.Node.GetLocation(), fieldDeclaration.Declaration.Variables.ElementAt(index).Identifier.Text));
                }
                ++index;
            }
        }
    }
}
