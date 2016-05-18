﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using Microsoft.R.Components.Controller;
using Microsoft.R.Components.Test.Assertions;

namespace Microsoft.R.Components.Test {
    [ExcludeFromCodeCoverage]
    public static class AssertionExtensions {
        public static AsyncCommandAssertions Should(this IAsyncCommand command) {
            return new AsyncCommandAssertions(command);
        }
    }
}