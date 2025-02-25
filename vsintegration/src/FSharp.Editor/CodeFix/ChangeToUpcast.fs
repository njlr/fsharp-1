﻿// Copyright (c) Microsoft Corporation.  All Rights Reserved.  See License.txt in the project root for license information.

namespace Microsoft.VisualStudio.FSharp.Editor

open System.Composition
open System.Threading.Tasks
open System.Collections.Immutable

open Microsoft.CodeAnalysis.Text
open Microsoft.CodeAnalysis.CodeFixes

[<ExportCodeFixProvider(FSharpConstants.FSharpLanguageName, Name = CodeFix.ChangeToUpcast); Shared>]
type internal FSharpChangeToUpcastCodeFixProvider() =
    inherit CodeFixProvider()

    override _.FixableDiagnosticIds = ImmutableArray.Create("FS3198")

    override this.RegisterCodeFixesAsync context : Task =
        asyncMaybe {
            let! sourceText = context.Document.GetTextAsync(context.CancellationToken)
            let text = sourceText.GetSubText(context.Span).ToString()

            // Only works if it's one or the other
            let isDowncastOperator = text.Contains(":?>")
            let isDowncastKeyword = text.Contains("downcast")

            do!
                Option.guard (
                    (isDowncastOperator || isDowncastKeyword)
                    && not (isDowncastOperator && isDowncastKeyword)
                )

            let replacement =
                if isDowncastOperator then
                    text.Replace(":?>", ":>")
                else
                    text.Replace("downcast", "upcast")

            let title =
                if isDowncastOperator then
                    SR.UseUpcastOperator()
                else
                    SR.UseUpcastKeyword()

            do context.RegisterFsharpFix(CodeFix.ChangeToUpcast, title, [| TextChange(context.Span, replacement) |])
        }
        |> Async.Ignore
        |> RoslynHelpers.StartAsyncUnitAsTask(context.CancellationToken)
