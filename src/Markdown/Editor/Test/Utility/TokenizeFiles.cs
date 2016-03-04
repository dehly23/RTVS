﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using FluentAssertions;
using Microsoft.Common.Core.Test.Utility;
using Microsoft.Languages.Core.Test.Utility;
using Microsoft.Languages.Core.Text;
using Microsoft.Languages.Core.Tokens;

namespace Microsoft.Markdown.Editor.Test.Utility {
    [ExcludeFromCodeCoverage]
    public static class TokenizeFiles {
        // change to true in debugger if you want all baseline tree files regenerated
        private static bool _regenerateBaselineFiles = false;

        public static void TokenizeFile<TToken, TTokenType, TTokenizer>(MarkdownTestFilesFixture fixture, string name, string language)
            where TTokenizer : ITokenizer<TToken>, new()
            where TToken : IToken<TTokenType> {
            Action a = () => TokenizeFileImplementation<TToken, TTokenType, TTokenizer>(fixture, name);
            a.ShouldNotThrow();
        }

        private static void TokenizeFileImplementation<TToken, TTokenType, TTokenizer>(MarkdownTestFilesFixture fixture, string name)
            where TTokenizer : ITokenizer<TToken>, new() where TToken : IToken<TTokenType> {
            string testFile = fixture.GetDestinationPath(name);
            string baselineFile = testFile + ".tokens";
            string text = fixture.LoadDestinationFile(name);

            ITextProvider textProvider = new TextStream(text);
            var tokenizer = new TTokenizer();

            var tokens = tokenizer.Tokenize(textProvider, 0, textProvider.Length);
            string actual = DebugWriter.WriteTokens<TToken, TTokenType>(tokens);

            if (_regenerateBaselineFiles) {
                baselineFile = Path.Combine(fixture.SourcePath, @"Tokenization\", Path.GetFileName(testFile)) + ".tokens";
                TestFiles.UpdateBaseline(baselineFile, actual);
            } else {
                TestFiles.CompareToBaseLine(baselineFile, actual);
            }
        }
    }
}
