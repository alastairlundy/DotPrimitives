/*
    Resyslib
    Copyright (c) 2024 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;

namespace Resyslib.Runtime.Specifics
{
    public class DarwinPlatform : Platform, ICloneable, IEquatable<DarwinPlatform>
    {
        public Version DarwinVersion { get; }
        

        public DarwinPlatform(string name, Version operatingSystemVersion, Version kernelVersion, Version darwinVersion) : base(name, operatingSystemVersion, kernelVersion, PlatformFamily.Darwin)
        {
            DarwinVersion = darwinVersion;
        }

        public bool Equals(DarwinPlatform? other)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((DarwinPlatform)obj);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}