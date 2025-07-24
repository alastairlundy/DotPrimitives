﻿/*
    AlastairLundy.DotPrimitives 
    Copyright (C) 2024-2025  Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
   */


using System;

using AlastairLundy.DotPrimitives.Processes;

#if NET5_0_OR_GREATER
#else
using System.Runtime.InteropServices;
#endif

namespace AlastairLundy.DotPrimitives.Extensions.Processes;

/// <summary>
/// 
/// </summary>
public static class IsSupportedOnOsExtensions
{
    /// <summary>
    /// Returns whether UserCredential is supported on the currently running Operating System.
    /// </summary>
    /// <param name="userCredential"></param>
    /// <returns>True if supported; false otherwise.</returns>
    public static bool IsSupportedOnCurrentOS(this UserCredential userCredential)
    {
#if NET5_0_OR_GREATER
        return OperatingSystem.IsWindows();
#else
        return RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
#endif
    }
}