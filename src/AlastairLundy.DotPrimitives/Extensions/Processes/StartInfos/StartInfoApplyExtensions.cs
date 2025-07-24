﻿/*
    AlastairLundy.DotPrimitives 
    Copyright (C) 2024-2025  Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
   */


using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using AlastairLundy.DotPrimitives.Processes;

#if NETSTANDARD2_0 || NETSTANDARD2_1
using OperatingSystem = Polyfills.OperatingSystemPolyfill;
#else
using System.Runtime.Versioning;
#endif

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
// ReSharper disable UnusedMethodReturnValue.Global

namespace AlastairLundy.DotPrimitives.Extensions.Processes.StartInfos;

public static class StartInfoApplyExtensions
{
    /// <summary>
    /// Adds the specified Credential to the current ProcessStartInfo object.
    /// </summary>
    /// <param name="processStartInfo">The current ProcessStartInfo object.</param>
    /// <param name="credential">The credential to be added.</param>
#if NET5_0_OR_GREATER
        [SupportedOSPlatform("windows")]
#endif
    public static void ApplyUserCredential(this ProcessStartInfo processStartInfo, UserCredential credential)
    {
#pragma warning disable CA1416
        if (credential.IsSupportedOnCurrentOS())
        {
            if (credential.Domain is not null)
            {
                processStartInfo.Domain = credential.Domain;
            }

            if (credential.UserName is not null)
            {
                processStartInfo.UserName = credential.UserName;
            }

            if (credential.Password is not null)
            {
                processStartInfo.Password = credential.Password;
            }

            if (credential.LoadUserProfile is not null)
            {
                processStartInfo.LoadUserProfile = (bool)credential.LoadUserProfile;
            }
        }
        else
        {
            throw new PlatformNotSupportedException();
        }
#pragma warning restore CA1416
    }
        
    /// <summary>
    /// Attempts to add the specified Credential to the current ProcessStartInfo object.
    /// </summary>
    /// <param name="processStartInfo">The current Process start info object.</param>
    /// <param name="credential">The credential to be added.</param>
    /// <returns>True if successfully applied; false otherwise.</returns>
    public static bool TryApplyUserCredential(this ProcessStartInfo processStartInfo, UserCredential credential)
    {
        if (credential.IsSupportedOnCurrentOS())
        {
            try
            {
#pragma warning disable CA1416
                ApplyUserCredential(processStartInfo, credential);
#pragma warning restore CA1416
                return true;
            }
            catch
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    
    /// <summary>
    /// Applies environment variables to a specified ProcessStartInfo object.
    /// </summary>
    /// <param name="processStartInfo">The ProcessStartInfo object to apply environment variables to.</param>
    /// <param name="environmentVariables">A dictionary of environment variable names and their corresponding values.</param>
    public static void ApplyEnvironmentVariables(this ProcessStartInfo processStartInfo,
        IReadOnlyDictionary<string, string> environmentVariables)
    {
        if (environmentVariables.Count > 0)
        {
            foreach (KeyValuePair<string, string> variable in environmentVariables)
            {
                if (variable.Value is not null)
                {
                    processStartInfo.Environment[variable.Key] = variable.Value;
                }
            }
        }
    }
        
    /// <summary>
    /// Applies a requirement to run the process start info as an administrator.
    /// </summary>
    /// <param name="processStartInfo"></param>
    public static void RunAsAdministrator(this ProcessStartInfo processStartInfo)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            processStartInfo.Verb = "runas";
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX) || OperatingSystem.IsFreeBSD())
        {
            processStartInfo.Verb = "sudo";
        }
    }
}