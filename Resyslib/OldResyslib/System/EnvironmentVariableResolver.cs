using System;
using System.IO;
using AlastairLundy.Resyslib.Abstractions;
using AlastairLundy.Resyslib.Runtime;

namespace AlastairLundy.Resyslib
{
    public class EnvironmentVariableResolver : IEnvironmentVariableResolver
    {
        /// <summary>
        /// An operating system specific PATH Environment Variable separator.
        /// </summary>
        public char PathEnvironmentVariableSeparator
        {
            get
            {
                if (OperatingSystemPolyfill.IsWindows())
                {
                    return ';';
                }
                else
                {
                    return ':';
                }
            }
        }

        public bool DoesEnvironmentVariableExist(string variableName)
        {
            throw new System.NotImplementedException();
        }

        public string GetEnvironmentVariable(string variableName)
        {
            throw new System.NotImplementedException();
        }

        public bool DoesFilePathContainPathEnvironmentVariable(string filePath)
        {
            bool output = false;
            
            var pathResult = Environment.GetEnvironmentVariable("PATH");

            if (pathResult == null)
            {
                throw new NullReferenceException("PATH environment variable was not found");
            }
            
            string[] pathVariables = pathResult.Split(PathEnvironmentVariableSeparator);

            foreach (string pathVariable in pathVariables)
            {
                if (filePath.StartsWith(pathVariable) || filePath.Contains(pathVariable))
                {
                    output = true;
                }
            }
            
            return output;
        }

        public bool DoesFilePathContainPathEnvironmentVariable(string filePath, string variableName)
        {
            throw new System.NotImplementedException();
        }
    }
}