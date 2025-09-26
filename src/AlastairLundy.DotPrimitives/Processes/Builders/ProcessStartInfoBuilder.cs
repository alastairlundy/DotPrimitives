

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;

namespace AlastairLundy.DotPrimitives.Processes.Builders;

/// <summary>
/// 
/// </summary>
[SuppressMessage("Interoperability", "CA1416:Validate platform compatibility")]
public class ProcessStartInfoBuilder : IProcessStartInfoBuilder
{
    private readonly ProcessStartInfo _processStartInfo;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="targetFilePath"></param>
    public ProcessStartInfoBuilder(string targetFilePath)
    {
        _processStartInfo = new ProcessStartInfo
        {
            FileName = targetFilePath
        };
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="processStartInfo"></param>
    /// <param name="environment"></param>
    protected ProcessStartInfoBuilder(ProcessStartInfo processStartInfo, IDictionary<string, string?> environment)
    {
        _processStartInfo = processStartInfo;

        if (_processStartInfo.Environment.Equals(environment) == false)
        {
            foreach (KeyValuePair<string, string?> environmentVariable in environment)
            {
                if (environmentVariable.Value is not null)
                {
                    _processStartInfo.Environment[environmentVariable.Key] = environmentVariable.Value;
                }
            }   
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="arguments"></param>
    /// <returns></returns>
    [Pure]
    public IProcessStartInfoBuilder WithArguments(IEnumerable<string> arguments)
    {
        return WithArguments(arguments,
            false);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="arguments"></param>
    /// <param name="escapeArguments"></param>
    /// <returns></returns>
    [Pure]
    public IProcessStartInfoBuilder WithArguments(IEnumerable<string> arguments, bool escapeArguments)
    {
        string args;
        
        if (escapeArguments == false)
        {
            args = string.Join(" ", arguments);
        }
        else
        {
            args = arguments.Where(x => )
        }

        return WithArguments(args);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="arguments"></param>
    /// <returns></returns>
    [Pure]
    public IProcessStartInfoBuilder WithArguments(string arguments)
    {
        string newArgs;
        
        if (string.IsNullOrEmpty(_processStartInfo.Arguments))
        {
            newArgs = arguments;
        }
        else
        {
            newArgs = $"{_processStartInfo.Arguments} {arguments}";       
        }        
        
        return new ProcessStartInfoBuilder(
            new ProcessStartInfo {
                FileName = _processStartInfo.FileName,
                CreateNoWindow = _processStartInfo.CreateNoWindow,
                Arguments = newArgs,
                WorkingDirectory = _processStartInfo.WorkingDirectory,
                Verb = _processStartInfo.Verb,
                Domain = _processStartInfo.Domain,
                UserName = _processStartInfo.UserName,
                Password = _processStartInfo.Password,
                LoadUserProfile = _processStartInfo.LoadUserProfile,
                UseShellExecute = _processStartInfo.UseShellExecute,
                RedirectStandardInput = _processStartInfo.RedirectStandardInput,
                RedirectStandardOutput = _processStartInfo.RedirectStandardOutput,
                RedirectStandardError = _processStartInfo.RedirectStandardError,
                StandardInputEncoding = _processStartInfo.StandardInputEncoding,
                StandardOutputEncoding = _processStartInfo.StandardOutputEncoding,
                StandardErrorEncoding = _processStartInfo.StandardErrorEncoding,
            }, _processStartInfo.Environment);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="targetFilePath"></param>
    /// <returns></returns>
    [Pure]
    public IProcessStartInfoBuilder WithTargetFile(string targetFilePath)
    {
        return new ProcessStartInfoBuilder(
            new ProcessStartInfo {
                FileName = _processStartInfo.FileName,
                CreateNoWindow = _processStartInfo.CreateNoWindow,
                Arguments = _processStartInfo.Arguments,
                WorkingDirectory = _processStartInfo.WorkingDirectory,
                Verb = _processStartInfo.Verb,
                Domain = _processStartInfo.Domain,
                UserName = _processStartInfo.UserName,
                Password = _processStartInfo.Password,
                LoadUserProfile = _processStartInfo.LoadUserProfile,
                UseShellExecute = _processStartInfo.UseShellExecute,
                RedirectStandardInput = _processStartInfo.RedirectStandardInput,
                RedirectStandardOutput = _processStartInfo.RedirectStandardOutput,
                RedirectStandardError = _processStartInfo.RedirectStandardError,
                StandardInputEncoding = _processStartInfo.StandardInputEncoding,
                StandardOutputEncoding = _processStartInfo.StandardOutputEncoding,
                StandardErrorEncoding = _processStartInfo.StandardErrorEncoding,
            }, _processStartInfo.Environment);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="environmentVariables"></param>
    /// <returns></returns>
    [Pure]
    public IProcessStartInfoBuilder WithEnvironmentVariables(IDictionary<string, string?> environmentVariables)
    {
        return new ProcessStartInfoBuilder(
            new ProcessStartInfo {
                FileName = _processStartInfo.FileName,
                CreateNoWindow = _processStartInfo.CreateNoWindow,
                Arguments = _processStartInfo.Arguments,
                WorkingDirectory = _processStartInfo.WorkingDirectory,
                Verb = _processStartInfo.Verb,
                Domain = _processStartInfo.Domain,
                UserName = _processStartInfo.UserName,
                Password = _processStartInfo.Password,
                LoadUserProfile = _processStartInfo.LoadUserProfile,
                UseShellExecute = _processStartInfo.UseShellExecute,
                RedirectStandardInput = _processStartInfo.RedirectStandardInput,
                RedirectStandardOutput = _processStartInfo.RedirectStandardOutput,
                RedirectStandardError = _processStartInfo.RedirectStandardError,
                StandardInputEncoding = _processStartInfo.StandardInputEncoding,
                StandardOutputEncoding = _processStartInfo.StandardOutputEncoding,
                StandardErrorEncoding = _processStartInfo.StandardErrorEncoding,
            }, environmentVariables);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="runAsAdministrator"></param>
    /// <returns></returns>
    [Pure]
    public IProcessStartInfoBuilder WithAdministratorPrivileges(bool runAsAdministrator)
    {
   
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="workingDirectoryPath"></param>
    /// <returns></returns>
    [Pure]
    public IProcessStartInfoBuilder WithWorkingDirectory(string workingDirectoryPath)
    {
      
    }

    
    public IProcessStartInfoBuilder WithDomain(string domain)
    {
        throw new NotImplementedException();
    }

    public IProcessStartInfoBuilder WithUsername(string userName)
    {
        throw new NotImplementedException();
    }

    public IProcessStartInfoBuilder WithPassword(SecureString password)
    {
        throw new NotImplementedException();
    }

    public IProcessStartInfoBuilder WithUserProfileLoading(bool loadUserProfile)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    [Pure]
    public IProcessStartInfoBuilder WithStandardInputPipe(StreamWriter source)
    {
   
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    [Pure]
    public IProcessStartInfoBuilder WithStandardOutputPipe(StreamReader target)
    {
        
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    [Pure]
    public IProcessStartInfoBuilder WithStandardErrorPipe(StreamReader target)
    {
   
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="useShellExecution"></param>
    /// <returns></returns>
    [Pure]
    public IProcessStartInfoBuilder WithShellExecution(bool useShellExecution)
    {
      
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="enableWindowCreation"></param>
    /// <returns></returns>
    [Pure]
    public IProcessStartInfoBuilder WithWindowCreation(bool enableWindowCreation)
    {
        return new ProcessStartInfoBuilder(
            new ProcessStartInfo
            {
                
                
                CreateNoWindow = enableWindowCreation == false,
            }, TODO);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="standardInputEncoding"></param>
    /// <param name="standardOutputEncoding"></param>
    /// <param name="standardErrorEncoding"></param>
    /// <returns></returns>
    [Pure]
    public IProcessStartInfoBuilder WithEncoding(Encoding standardInputEncoding = null, Encoding standardOutputEncoding = null,
        Encoding standardErrorEncoding = null)
    {
   
    }

    /// <summary>
    /// Builds and returns a ProcessStartInfo object with the specified properties.
    /// </summary>
    /// <returns>The configured ProcessStartInfo object.</returns>
    public ProcessStartInfo Build()
    { 
        ProcessStartInfo processStartInfo = new ProcessStartInfo
        {
            FileName = _processStartInfo.FileName,
            Arguments =  _processStartInfo.Arguments,
            WorkingDirectory = _processStartInfo.WorkingDirectory,
            UseShellExecute = _processStartInfo.UseShellExecute,
            CreateNoWindow = _processStartInfo.CreateNoWindow,
            RedirectStandardInput = _processStartInfo.RedirectStandardInput,
            RedirectStandardOutput = _processStartInfo.RedirectStandardOutput,
            RedirectStandardError = _processStartInfo.RedirectStandardError,
            UserName = _processStartInfo.UserName,
#pragma warning disable CA1416
            Domain = _processStartInfo.Domain,
            Password = _processStartInfo.Password,
            LoadUserProfile = _processStartInfo.LoadUserProfile,
#pragma warning restore CA1416
            StandardOutputEncoding = _processStartInfo.StandardOutputEncoding,
            StandardErrorEncoding = _processStartInfo.StandardErrorEncoding,
            Verb = _processStartInfo.Verb,
        };
        
        if (string.IsNullOrEmpty(_processStartInfo.Arguments) == false) 
            processStartInfo.Arguments = _processStartInfo.Arguments;

        foreach (var environmentVariable in _processStartInfo.Environment)
        {
            processStartInfo.Environment[environmentVariable.Key] = environmentVariable.Value;
        }
        
        #if NET8_0_OR_GREATER
        if (processStartInfo.RedirectStandardInput) 
            processStartInfo.StandardInputEncoding = _processStartInfo.StandardInputEncoding;
        #endif
        
        return processStartInfo;
    }
}