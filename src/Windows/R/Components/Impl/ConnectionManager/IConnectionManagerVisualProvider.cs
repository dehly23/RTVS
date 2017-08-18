﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Microsoft.R.Components.ConnectionManager {
    public interface IConnectionManagerVisualProvider {
        IConnectionManagerVisual GetOrCreate(IConnectionManager connectionManager, int instance = 0);
    }
}