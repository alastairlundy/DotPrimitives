/*
    Resyslib
    Copyright (c) 2024 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;
// ReSharper disable MemberCanBePrivate.Global

namespace Resyslib.Runtime
{
    /// <summary>
    /// 
    /// </summary>
    public class Platform : ICloneable, IEquatable<Platform>
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; }
        
        /// <summary>
        /// 
        /// </summary>
        public Version OperatingSystemVersion { get; }
        
        /// <summary>
        /// 
        /// </summary>
        public Version KernelVersion { get; }

        /// <summary>
        /// 
        /// </summary>
        public Resyslib.Runtime.PlatformFamily Family { get; }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="operatingSystemVersion"></param>
        /// <param name="kernelVersion"></param>
        /// <param name="family"></param>
        public Platform(string name, Version operatingSystemVersion, Version kernelVersion, Resyslib.Runtime.PlatformFamily family)
        {
            this.Name = name;
            this.OperatingSystemVersion = operatingSystemVersion;
            this.KernelVersion = kernelVersion;
            this.Family = family;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return new Platform(Name, OperatingSystemVersion, KernelVersion, Family);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Platform? other)
        {
            if (other == null)
            {
                return false;
            }

            return OperatingSystemVersion.Equals(other.OperatingSystemVersion) 
                   && KernelVersion.Equals(other.KernelVersion)
                   && Family.Equals(other.Family)
                   && Name.Equals(other.Name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            else
            {
                return Equals((Platform)obj);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Name, OperatingSystemVersion, KernelVersion, Family);
        }
    }
}