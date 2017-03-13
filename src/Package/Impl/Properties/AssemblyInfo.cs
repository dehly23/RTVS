﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Microsoft.VisualStudio.Shell;
using System.Runtime.CompilerServices;

[assembly: ProvideBindingRedirection(AssemblyName = "Microsoft.Win32.Primitives",
    OldVersionLowerBound = "0.0.0.0", OldVersionUpperBound = "4.0.2.0", NewVersion = "4.0.2.0")]
[assembly: ProvideBindingRedirection(AssemblyName = "System.Diagnostics.DiagnosticSource",
    OldVersionLowerBound = "0.0.0.0", OldVersionUpperBound = "4.0.1.0", NewVersion = "4.0.1.0")]
[assembly: ProvideBindingRedirection(AssemblyName = "System.Net.Http",
    OldVersionLowerBound = "0.0.0.0",  OldVersionUpperBound = "4.1.1.0", NewVersion = "4.1.1.0")]

#if SIGN
[assembly: InternalsVisibleTo("Microsoft.VisualStudio.R.Package.Test, PublicKey=002400000480000094000000060200000024000052534131000400000100010007D1FA57C4AED9F0A32E84AA0FAEFD0DE9E8FD6AEC8F87FB03766C834C99921EB23BE79AD9D5DCC1DD9AD236132102900B723CF980957FC4E177108FC607774F29E8320E92EA05ECE4E821C0A5EFE8F1645C4C0C93C1AB99285D622CAA652C1DFAD63D745D6F2DE5F17E5EAF0FC4963D261C8A12436518206DC093344D5AD293")]
[assembly: InternalsVisibleTo("Microsoft.VisualStudio.R.Interactive.Test, PublicKey=002400000480000094000000060200000024000052534131000400000100010007D1FA57C4AED9F0A32E84AA0FAEFD0DE9E8FD6AEC8F87FB03766C834C99921EB23BE79AD9D5DCC1DD9AD236132102900B723CF980957FC4E177108FC607774F29E8320E92EA05ECE4E821C0A5EFE8F1645C4C0C93C1AB99285D622CAA652C1DFAD63D745D6F2DE5F17E5EAF0FC4963D261C8A12436518206DC093344D5AD293")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2, PublicKey=0024000004800000940000000602000000240000525341310004000001000100c547cac37abd99c8db225ef2f6c8a3602f3b3606cc9891605d02baa56104f4cfc0734aa39b93bf7852f7d9266654753cc297e7d2edfe0bac1cdcf9f717241550e0a7b191195b7667bb4f64bcb8e2121380fd1d9d46ad2d92d2d15605093924cceaf74c4861eff62abf69b9291ed0a340e113be11e6a7d3113e92484cf7045cc7")]
#else
[assembly: InternalsVisibleTo("Microsoft.VisualStudio.R.Package.Test")]
[assembly: InternalsVisibleTo("Microsoft.VisualStudio.R.Interactive.Test")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
#endif

