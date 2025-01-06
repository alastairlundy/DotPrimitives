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
        public async Task<Platform> GetCurrentPlatformAsync()
        {
            Version platformVersion = await GetOsVersionAsync();
            
            
            Platform platform = new Platform(, platformVersion, platformVersion, PlatformFamily.WindowsNT);
        }

        private async Task<Version> GetOsVersionAsync()
        {
      
        }
    }
}