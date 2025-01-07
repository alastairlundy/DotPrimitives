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
using CliRunner.Commands.Buffered;
using Resyslib.Runtime.Specifics;

namespace Resyslib.Runtime.Providers
{
    public class AndroidPlatformProvider : IAndroidPlatformProvider
    {
        public async Task<Platform> GetCurrentPlatformAsync()
        {
            return new Platform(
                await GetPlatformNameAsync(),
                await GetPlatformVersionAsync(),
                await GetPlatformKernelVersionAsync(),
                PlatformFamily.Android);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
#if NET5_0_OR_GREATER
        [SupportedOSPlatform("android")]
#endif
        public async Task<AndroidPlatform> GetCurrentAndroidPlatformAsync()
        {
            return new AndroidPlatform(await GetPlatformNameAsync(),
                await GetPlatformVersionAsync(),
                await GetPlatformKernelVersionAsync(),
                await GetSdkLevelAsync(),
                await GetCodeNameAsync());
        }

        private async Task<string> GetPlatformNameAsync()
        {
            throw new NotImplementedException();
        }

        private async Task<Version> GetPlatformVersionAsync()
        {
            throw new NotImplementedException();
        }

        private async Task<Version> GetPlatformKernelVersionAsync()
        {
            throw new NotImplementedException();
        }

        private async Task<Version> GetSdkLevelAsync()
        {
#pragma warning disable CA1416
            BufferedCommandResult result = await Cli.Run("getprop")
                .WithArguments("ro.build.version.release")
                .ExecuteBufferedAsync();
#pragma warning restore CA1416

            string version = result.StandardOutput.Replace(" ", string.Empty);
            
            return Version.Parse(version);
        }

        private async Task<string> GetCodeNameAsync()
        {
        }
    }
}