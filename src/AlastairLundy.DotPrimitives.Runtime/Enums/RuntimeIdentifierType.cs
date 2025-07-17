/*
      OldResyslib
      Copyright (c) 2024 Alastair Lundy

      This Source Code Form is subject to the terms of the Mozilla Public
      License, v. 2.0. If a copy of the MPL was not distributed with this
      file, You can obtain one at http://mozilla.org/MPL/2.0/.
   */

namespace AlastairLundy.DotPrimitives.Runtime.Enums;

/// <summary>
/// The type of RuntimeIdentifier generated or detected.
/// </summary>
public enum RuntimeIdentifierType
{
    /// <summary>
    /// A Runtime Identifier that is valid for all architectures of an operating system.
    /// </summary>
    AnyGeneric,
    /// <summary>
    /// A Runtime Identifier that is valid for all supported versions of the OS being run.
    /// </summary>
    Generic,
    /// <summary>
    /// A Runtime Identifier that is valid for the specified OS and specified OS version being run.
    /// </summary>
    Specific,
    /// <summary>
    /// This is meant for Linux use only. DO NOT USE ON WINDOWS or MAC.
    /// </summary>
    DistroSpecific,
    /// <summary>
    /// This is meant for Linux use only. DO NOT USE ON WINDOWS or MAC.
    /// </summary>
    VersionLessDistroSpecific
}