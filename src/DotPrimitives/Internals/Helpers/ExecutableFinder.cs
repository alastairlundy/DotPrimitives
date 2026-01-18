namespace DotPrimitives.Internals.Helpers;

internal static class ExecutableFinder
{
    internal static string? FindExecutable(string fileName)
    {
        return OperatingSystem.IsWindows() ? FindExecutableOnWindows(fileName) :
            FindExecutableOnUnix(fileName);
    }

    private static string? FindExecutableOnUnix(string fileName)
    {
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = File.Exists("/usr/bin/which") ? "/usr/bin/which" : "which",
            Arguments = fileName,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            WorkingDirectory = Environment.CurrentDirectory
        };

        using ProcessWrapper wrapper = new ProcessWrapper(startInfo);

        try
        {
            wrapper.Start();

            Task<(string standardOut, string standardError)> waitForExit = wrapper.
                WaitForBufferedExitAsync(CancellationToken.None);

            waitForExit.Wait();

            string[] lines = waitForExit.Result.standardOut.Split(Environment.NewLine)
                .Where(l => !string.IsNullOrEmpty(l))
                .ToArray();
            
            return lines.FirstOrDefault();
        }
        catch
        {
            return null;
        }
    }
    
    private static string? FindExecutableOnWindows(string fileName)
    {
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = $"{Environment.GetFolderPath(Environment.SpecialFolder.Windows)}/System32/where.exe",
            Arguments = fileName,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            WorkingDirectory = Environment.CurrentDirectory
        };

        using ProcessWrapper wrapper = new ProcessWrapper(startInfo);

        try
        {
            wrapper.Start();

            Task<(string standardOut, string standardError)> waitForExit = wrapper.
                WaitForBufferedExitAsync(CancellationToken.None);

            waitForExit.Wait();

            string[] lines = waitForExit.Result.standardOut.Split(Environment.NewLine)
                .Where(l => !string.IsNullOrEmpty(l))
                .ToArray();
            
            return lines.FirstOrDefault();
        }
        catch
        {
            return null;
        }
    }
}