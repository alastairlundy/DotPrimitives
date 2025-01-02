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
    public class PlatformProviderSelector : IPlatformProviderSelector
    {
        public IPlatformProvider GetApplicablePlatformProvider(PlatformFamily platformFamily)
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

        public bool TryGetApplicablePlatformProvider(PlatformFamily platformFamily, out IPlatformProvider? provider)
        {
            throw new System.NotImplementedException();
        }
    }
}