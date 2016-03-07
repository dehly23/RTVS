﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Microsoft.VisualStudio.Editor;
using Microsoft.VisualStudio.R.Package.Shell;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.TextManager.Interop;

namespace Microsoft.VisualStudio.R.Package.Utilities {
    public static class TextBufferUtilities {
        private static IVsEditorAdaptersFactoryService _adaptersFactoryService;
        private static IVsEditorAdaptersFactoryService AdaptersFactoryService => 
            _adaptersFactoryService ?? (_adaptersFactoryService = VsAppShell.Current.ExportProvider.GetExportedValue<IVsEditorAdaptersFactoryService>());

        public static T As<T>(this ITextBuffer textBuffer) where T : class {
            var t = textBuffer as T;
            if (t == null) {
                var vsTextBuffer = AdaptersFactoryService.GetBufferAdapter(textBuffer);
                return vsTextBuffer as T;
            }
            return t;
        }

        public static ITextBuffer ToITextBuffer(this IVsTextBuffer vsTextBuffer) {
            return AdaptersFactoryService.GetDocumentBuffer(vsTextBuffer);
        }

        public static ITextBuffer ToITextBuffer(this IVsTextLayer vsTextLayer) {
            IVsTextLines vsTextLines;
            vsTextLayer.GetBaseBuffer(out vsTextLines);

            return vsTextLines.ToITextBuffer();
        }

        public static ITextBuffer ToITextBuffer(this IVsTextLines vsTextLines) {
            return ToITextBuffer(vsTextLines as IVsTextBuffer);
        }
    }
}
