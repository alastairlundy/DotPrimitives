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
    public class AndroidPlatform : Platform, ICloneable, IEquatable<AndroidPlatform>
    {
        public int SdkLevel { get; }
        
        /// <summary>
        /// The Android version code name.
        /// </summary>
        public string VersionCodeName { get; }
        
        public AndroidPlatform(string name, Version operatingSystemVersion, Version kernelVersion, int sdkLevel, string versionCodeName) : base(name, operatingSystemVersion, kernelVersion, PlatformFamily.Android)
        {
            this.SdkLevel = sdkLevel;
            this.VersionCodeName = versionCodeName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(AndroidPlatform? other)
        {
            if (other == null)
            {
                return false;
            }
            
            return this.SdkLevel.Equals(other.SdkLevel) &&
                   this.VersionCodeName.Equals(other.VersionCodeName) &&
                   this.Name.Equals(other.Name) &&
                   this.OperatingSystemVersion.Equals(other.OperatingSystemVersion) &&
                   this.KernelVersion.Equals(other.KernelVersion) &&
                   this.Family.Equals(other.Family);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return Equals((AndroidPlatform)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.SdkLevel, this.VersionCodeName,
                this.Name, this.OperatingSystemVersion, this.KernelVersion, this.Family);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public new object Clone()
        {
            return new AndroidPlatform(Name, OperatingSystemVersion, KernelVersion, SdkLevel, VersionCodeName);
        }
    }
}