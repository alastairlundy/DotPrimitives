namespace DotPrimitives.Internals.Helpers;

internal class ProcessWrapper : Process
{
    internal ProcessWrapper(ProcessStartInfo startInfo)
    {
        EnableRaisingEvents = true;
        Exited += OnExited;
        startInfo.RedirectStandardOutput = true;
        startInfo.RedirectStandardError = true;
        startInfo.WorkingDirectory = Environment.CurrentDirectory;
    }

    private void OnExited(object sender, EventArgs e)
    {
        ExitCode = base.ExitCode;
    }

    internal bool HasStarted { get; private set; }
    
    internal new int ExitCode { get; private set; }
    
    internal new bool Start()
    {
        bool success = base.Start();
        
        HasStarted = success;
        return success;
    }

    internal async Task<(string standardOut, string standardError)> WaitForBufferedExitAsync(
        CancellationToken cancellationToken)
    {
        Task<string> standardOut =  StandardOutput.ReadToEndAsync(cancellationToken);
        Task<string> standardError = StandardError.ReadToEndAsync(cancellationToken);

        Task waitForExit = this.WaitForExitAsync(cancellationToken);
        
        await Task.WhenAll([standardOut, standardError, 
            waitForExit]);
        
        return (standardOut.Result, standardError.Result);
    }
}