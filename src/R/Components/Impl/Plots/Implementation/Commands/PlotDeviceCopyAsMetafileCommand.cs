﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Common.Core;
using Microsoft.Common.Core.UI;
using Microsoft.Common.Core.UI.Commands;
using Microsoft.R.Components.InteractiveWorkflow;

namespace Microsoft.R.Components.Plots.Implementation.Commands {
    internal sealed class PlotDeviceCopyAsMetafileCommand : PlotDeviceCommand, IAsyncCommand {
        public PlotDeviceCopyAsMetafileCommand(IRInteractiveWorkflow interactiveWorkflow, IRPlotDeviceVisualComponent visualComponent)
            : base(interactiveWorkflow, visualComponent) {
        }

        public CommandStatus Status => HasCurrentPlot && !IsInLocatorMode ? CommandStatus.SupportedAndEnabled : CommandStatus.Supported;

        public async Task InvokeAsync() {
            var ui = InteractiveWorkflow.Shell.Services.GetService<IUIServices>();
            string filePath = Path.GetTempFileName();
            try {
                await InteractiveWorkflow.Plots.ExportToMetafileAsync(
                    VisualComponent.ActivePlot,
                    filePath,
                    PixelsToInches(VisualComponent.Device.PixelWidth),
                    PixelsToInches(VisualComponent.Device.PixelHeight),
                    VisualComponent.Device.Resolution);

                InteractiveWorkflow.Shell.DispatchOnUIThread(() => {
                    try {
                        var mf = new System.Drawing.Imaging.Metafile(filePath);
                        Clipboard.SetData(DataFormats.EnhancedMetafile, mf);
                    } catch (Exception e) when (!e.IsCriticalException()) {
                        ui.ShowErrorMessage(string.Format(Resources.Plots_CopyToClipboardError, e.Message));
                    } finally {
                        try {
                            File.Delete(filePath);
                        } catch (IOException) {
                        }
                    }
                });
            } catch (RPlotManagerException ex) {
                ui.ShowErrorMessage(ex.Message);
            } catch (OperationCanceledException) {
            }
        }

        private static double PixelsToInches(int pixels) => pixels / 96.0;
    }
}
