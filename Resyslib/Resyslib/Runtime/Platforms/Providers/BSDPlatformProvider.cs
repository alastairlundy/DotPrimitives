/*
    Resyslib
    Copyright (c) 2024 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

// ReSharper disable InconsistentNaming

using System;
using System.Linq;
using System.Threading.Tasks;

using CliRunner;
using CliRunner.Commands.Buffered;
using Resyslib.Internal.Localizations;
using Resyslib.Runtime.Abstractions;

namespace Resyslib.Runtime.Providers
{
    public class BSDPlatformProvider : IPlatformProvider
    {
        public async Task<Platform> GetCurrentPlatformAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task<Version> GetPlatformVersionAsync()
        {
            if (OperatingSystem.IsFreeBSD() == false)
            {
                throw new PlatformNotSupportedException(Resources.Exceptions_PlatformNotSupported_FreeBsdOnly);
            }

            BufferedCommandResult result = await Cli.Run("/usr/bin/uname")
                .WithArguments("-v")
                .WithWorkingDirectory(Environment.CurrentDirectory)
                .ExecuteBufferedAsync();
            
            string versionString = result.StandardOutput.Replace("FreeBSD", string.Empty)
                .Replace("BSD", string.Empty)
                .Split(' ').First().Replace("-release", string.Empty);
            
            return Version.Parse(versionString);
        }
    }
}