// Copyright (c) Microsoft Corporation.  All Rights Reserved.  See License.txt in the project root for license information.

namespace Microsoft.VisualStudio.FSharp.Editor

open System.Composition
open System.Threading.Tasks
open System.Collections.Immutable

open Microsoft.CodeAnalysis.CodeFixes
open Microsoft.CodeAnalysis.CodeActions

open FSharp.Compiler.Diagnostics

[<ExportCodeFixProvider(FSharpConstants.FSharpLanguageName, Name = "ProposeUpperCaseLabel"); Shared>]
type internal FSharpProposeUpperCaseLabelCodeFixProvider [<ImportingConstructor>] () =
    inherit CodeFixProvider()

    override _.FixableDiagnosticIds = ImmutableArray.Create("FS0053")

    override _.RegisterCodeFixesAsync context : Task =
        asyncMaybe {
            let textChanger (originalText: string) =
                originalText.[0].ToString().ToUpper() + originalText.Substring(1)

            let! solutionChanger, originalText = SymbolHelpers.changeAllSymbolReferences (context.Document, context.Span, textChanger)

            let title =
                CompilerDiagnostics.GetErrorMessage(FSharpDiagnosticKind.ReplaceWithSuggestion <| textChanger originalText)

            context.RegisterCodeFix(CodeAction.Create(title, solutionChanger, title), context.Diagnostics)
        }
        |> Async.Ignore
        |> RoslynHelpers.StartAsyncUnitAsTask(context.CancellationToken)
