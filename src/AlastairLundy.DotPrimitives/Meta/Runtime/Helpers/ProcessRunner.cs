

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