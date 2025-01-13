/*
    Resyslib
    Copyright (c) 2024 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;

using Resyslib.Runtime.Abstractions;
using Resyslib.Runtime.Providers;

namespace Resyslib.Runtime
{
    public class DefaultPlatformProviderFactory : IPlatformProviderFactory
    {

        public static DefaultPlatformProviderFactory CreateFactory()
        {
            return new DefaultPlatformProviderFactory();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="PlatformNotSupportedException"></exception>
        public IPlatformProvider CreatePlatformProvider()
        {
            PlatformFamily platformFamily;
            
            if (OperatingSystemDetector.IsWindows())
            {
                platformFamily = PlatformFamily.WindowsNT;
            }
            else if (OperatingSystemDetector.IsLinux())
            {
                platformFamily = PlatformFamily.Linux;
            }
            else if (OperatingSystemDetector.IsMacOS() ||
                     OperatingSystemDetector.IsIOS() ||
                     OperatingSystemDetector.IsTvOS() ||
                     OperatingSystemDetector.IsWatchOS() ||
                     OperatingSystemDetector.IsMacCatalyst() ||
                     OperatingSystemDetector.IsVisionOS())
            {
                platformFamily = PlatformFamily.Darwin;
            }
            else if (OperatingSystemDetector.IsFreeBSD())
            {
                platformFamily = PlatformFamily.BSD;
            }
            else if (OperatingSystemDetector.IsAndroid())
            {
                platformFamily = PlatformFamily.Android;
            }
            else if (OperatingSystemDetector.IsTizen())
            {
                platformFamily = PlatformFamily.Linux;
            }
            else if (OperatingSystemDetector.IsBrowser())
            {
                platformFamily = PlatformFamily.Other;
            }
            else
            {
                throw new PlatformNotSupportedException();
            }
            
            return CreatePlatformProvider(platformFamily);
        }

        public bool TryCreatePlatformProvider(out IPlatformProvider? platformProvider)
        {
            try
            {
                platformProvider = CreatePlatformProvider();
                return true;
            }
            catch
            {
                platformProvider = null;
                return false;
            }
        }

        public IPlatformProvider CreatePlatformProvider(PlatformFamily platformFamily)
        {
            return platformFamily switch
            {
                PlatformFamily.WindowsNT => new WindowsPlatformProvider(),
                PlatformFamily.Darwin => new DarwinPlatformProvider(),
                PlatformFamily.Linux => new LinuxPlatformProvider(),
                PlatformFamily.BSD => new BSDPlatformProvider(),
                PlatformFamily.Android => new AndroidPlatformProvider(),
                PlatformFamily.Other => throw new PlatformNotSupportedException(),
                PlatformFamily.Unix => new UnixPlatformProvider(),
                _ => throw new ArgumentOutOfRangeException(nameof(platformFamily), platformFamily, null)
            };
        }

        public bool TryCreatePlatformProvider(PlatformFamily platformFamily, out IPlatformProvider? provider)
        {
            try
            {
                provider = CreatePlatformProvider(platformFamily);
                return true;
            }
            catch
            {
                provider = null;
                return false;
            }
        }
    }
}