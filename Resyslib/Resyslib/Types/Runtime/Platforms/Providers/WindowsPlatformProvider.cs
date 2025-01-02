/*
    Resyslib
    Copyright (c) 2024 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;

using System.Threading.Tasks;

using CliRunner;

using Resyslib.Internal.Localizations;
using Resyslib.Runtime.Abstractions;

namespace Resyslib.Runtime.Providers
{
    public class WindowsPlatformProvider : IPlatformProvider
    {
        public Platform GetCurrentPlatform()
        {
            throw new System.NotImplementedException();
        }

        public Task<Platform> GetCurrentPlatformAsync()
        {
            throw new NotImplementedException();
        }

        private async Task<Version> GetOsVersionAsync()
        {
            if (OperatingSystem.IsFreeBSD() == false)
            {
                throw new PlatformNotSupportedException(Resources.Exceptions_PlatformNotSupported_FreeBsdOnly);
            }

            var result = await Cli.Run("/usr/bin/uname")
                .WithArguments("-v")
                .WithWorkingDirectory(Environment.CurrentDirectory)
                .ExecuteBufferedAsync();
            
            string versionString = result.StandardOutput.Replace("FreeBSD", string.Empty)
                .Split(' ')[0].Replace("-release", string.Empty);
            
            return Version.Parse(versionString);
        }
    }
}