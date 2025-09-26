/*
    MIT License
   
    Copyright (c) 2025 Alastair Lundy
   
    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:
   
    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.
   
    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.
 */

using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security;
using System.Text;

namespace AlastairLundy.DotPrimitives.Processes.Builders;

/// <summary>
/// An interface that defines fluent builder methods for configuring Process Start Information. 
/// </summary>
public interface IProcessStartInfoBuilder
{
    /// <summary>
    /// Sets the arguments to pass to the executable.
    /// </summary>
    /// <param name="arguments">The arguments to pass to the Process.</param>
    /// <returns>The updated IProcessStartInfoBuilder object with the specified arguments.</returns>
    IProcessStartInfoBuilder WithArguments(IEnumerable<string> arguments);
    
    /// <summary>
    /// Sets the arguments to pass to the executable.
    /// </summary>
    /// <param name="arguments">The arguments to pass to the executable.</param>
    /// <param name="escapeArguments">Whether to escape the arguments if escape characters are detected.</param>
    /// <returns>The new IProcessStartInfoBuilder object with the specified arguments.</returns>
    IProcessStartInfoBuilder WithArguments(IEnumerable<string> arguments, bool escapeArguments);

    /// <summary>
    /// Sets the arguments to pass to the executable.
    /// </summary>
    /// <param name="arguments">The arguments to pass to the executable.</param>
    /// <returns>The new IProcessStartInfoBuilder object with the specified arguments.</returns>
    IProcessStartInfoBuilder WithArguments(string arguments);
    
    /// <summary>
    /// Sets the Target File Path of the Process Executable.
    /// </summary>
    /// <param name="targetFilePath">The target file path of the Process.</param>
    /// <returns>The ProcessStartInfoBuilder with the updated Target File Path.</returns>
    IProcessStartInfoBuilder WithTargetFile(string targetFilePath);

    /// <summary>
    /// Sets the environment variables to be configured.
    /// </summary>
    /// <param name="environmentVariables">The environment variables to be configured.</param>
    /// <returns>The new ProcessStartInfoBuilder with the specified environment variables.</returns>
    IProcessStartInfoBuilder WithEnvironmentVariables(IDictionary<string, string?> environmentVariables);
    
    /// <summary>
    /// Sets whether to execute the Process with Administrator Privileges.
    /// </summary>
    /// <param name="runAsAdministrator">Whether to execute the Process with Administrator Privileges.</param>
    /// <returns>The new ProcessStartInfoBuilder with the specified Administrator Privileges settings.</returns>
    IProcessStartInfoBuilder WithAdministratorPrivileges(bool runAsAdministrator);

    /// <summary>
    /// Sets the working directory to be used for the Process.
    /// </summary>
    /// <param name="workingDirectoryPath">The working directory to be used.</param>
    /// <returns>The new ProcessStartInfoBuilder with the specified working directory.</returns>
    IProcessStartInfoBuilder WithWorkingDirectory(string workingDirectoryPath);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="domain"></param>
    /// <returns></returns>
    IProcessStartInfoBuilder WithDomain(string domain);

    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="userName"></param>
    /// <returns></returns>
    IProcessStartInfoBuilder WithUsername(string userName);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="password"></param>
    /// <returns></returns>
    IProcessStartInfoBuilder WithPassword(SecureString password);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="loadUserProfile"></param>
    /// <returns></returns>
    IProcessStartInfoBuilder WithUserProfileLoading(bool loadUserProfile);
    
    /// <summary>
    /// Sets the Standard Input Pipe source.
    /// </summary>
    /// <param name="source">The source to use for the Standard Input pipe.</param>
    /// <returns>The new ProcessStartInfoBuilder with the specified Standard Input pipe source.</returns>
    IProcessStartInfoBuilder WithStandardInputPipe(StreamWriter source);
    
    /// <summary>
    /// Sets the Standard Output Pipe target.
    /// </summary>
    /// <param name="target">The target to send the Standard Output to.</param>
    /// <returns>The new ProcessStartInfoBuilder with the specified Standard Output Pipe Target.</returns>
    IProcessStartInfoBuilder WithStandardOutputPipe(StreamReader target);
    
    /// <summary>
    /// Sets the Standard Error Pipe target.
    /// </summary>
    /// <param name="target">The target to send the Standard Error to.</param>
    /// <returns>The new ProcessStartInfoBuilder with the specified Standard Error Pipe Target.</returns>
    IProcessStartInfoBuilder WithStandardErrorPipe(StreamReader target);
    
    /// <summary>
    /// Enables or disables Process execution via Shell Execution.
    /// </summary>
    /// <param name="useShellExecution">Whether to enable or disable shell execution.</param>
    /// <returns>The new ProcessStartInfoBuilder with the specified shell execution behaviour.</returns>
    IProcessStartInfoBuilder WithShellExecution(bool useShellExecution);
    
    /// <summary>
    /// Enables or disables Window creation for the wrapped executable.
    /// </summary>
    /// <param name="enableWindowCreation">Whether to enable or disable window creation for the wrapped executable.</param>
    /// <returns>The new ProcessStartInfoBuilder with the specified window creation behaviour.</returns>
    IProcessStartInfoBuilder WithWindowCreation(bool enableWindowCreation);

    /// <summary>
    /// Sets the Encoding types to be used for Standard Input, Output, and Error.
    /// </summary>
    /// <param name="standardInputEncoding">The encoding type to be used for the Standard Input.</param>
    /// <param name="standardOutputEncoding">The encoding type to be used for the Standard Output.</param>
    /// <param name="standardErrorEncoding">The encoding type to be used for the Standard Error.</param>
    /// <returns>The new IProcessStartInfoBuilder with the specified Pipe Encoding types.</returns>
    IProcessStartInfoBuilder WithEncoding(Encoding? standardInputEncoding = null,
        Encoding? standardOutputEncoding = null,
        Encoding? standardErrorEncoding = null);

    /// <summary>
    /// Builds the Process configuration with the configured parameters.
    /// </summary>
    /// <returns>The newly configured Process configuration.</returns>
    ProcessStartInfo Build();
}