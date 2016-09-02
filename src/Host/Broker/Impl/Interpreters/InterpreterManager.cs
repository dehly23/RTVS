﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.Common.Core.IO;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.R.Interpreters;

namespace Microsoft.R.Host.Broker.Interpreters {
    public class InterpreterManager {
        private const string _localId = "local";

        private readonly ROptions _options;
        private readonly ILogger _logger;
        private IFileSystem _fs;
        public IReadOnlyCollection<Interpreter> Interpreters { get; private set; }

        [ImportingConstructor]
        public InterpreterManager(IOptions<ROptions> options, ILogger<InterpreterManager> logger) {
            _options = options.Value;
            _logger = logger;
        }

        public void Initialize(IFileSystem fs) {
            _fs = fs;

            if (_options.AutoDetect) {
                Interpreters = GetInterpreters().ToArray();
                var sb = new StringBuilder($"{Interpreters.Count} interpreters configured:");
                foreach (var interp in Interpreters) {
                    sb.Append(Environment.NewLine + $"'{interp.Id}': {interp.Version} at \"{interp.Path}\"");
                }
                _logger.LogInformation(sb.ToString());
            } else {
                var opt = _options.Interpreters.FirstOrDefault();
                if (!string.IsNullOrEmpty(opt.Value.BasePath)) {
                    var e = new RInterpreterInfo(string.Empty, opt.Value.BasePath);
                    if (e.VerifyInstallation()) { 
                        Interpreters = new List<Interpreter>() { new Interpreter(this, _localId, e.InstallPath, e.BinPath, e.Version) };
                    } else {
                        Debug.Fail("Specified interpreter us incompatible");
                    }
                } else {
                    Debug.Fail("Specified interpreter does not exist");
                }
            }
        }

        private IEnumerable<Interpreter> GetInterpreters() {
            _logger.LogTrace("Auto-detecting R ...");

            var engines = new RInstallation().GetCompatibleEngines();
            if (engines.Any()) {
                foreach (var e in engines) {
                    var detected = new Interpreter(this, _localId, e.InstallPath, e.BinPath, e.Version);
                    _logger.LogTrace($"R {detected.Version} detected at \"{detected.Path}\".");
                    yield return detected;
                }
            } else {
                _logger.LogWarning("No R interpreters found.");
            }
        }
    }
}

