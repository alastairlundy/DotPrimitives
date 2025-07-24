/*
    AlastairLundy.DotPrimitives
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System.Diagnostics;

namespace AlastairLundy.DotPrimitives.Meta.Runtime.Helpers;

    internal static class ProcessRunner
    {
        internal static Process CreateProcess(string targetFileName, string arguments)
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo
            {
                FileName = targetFileName,
                Arguments = arguments,
                RedirectStandardInput = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            Process output = new Process
            {
                StartInfo = processStartInfo
            };

            return output;
        }

        internal static string RunProcess(Process process)
        {
            process.Start();

            process.WaitForExit();

            string output = process.StandardOutput.ReadToEnd();
         
            process.Dispose();
            
            return output;
        }
    }