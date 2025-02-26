using Resyslib.Abstractions;

namespace Resyslib
{
    public class EnvironmentVariableResolver : IEnvironmentVariableResolver
    {
        public bool DoesEnvironmentVariableExist(string variableName)
        {
            throw new System.NotImplementedException();
        }

        public string GetEnvironmentVariable(string variableName)
        {
            throw new System.NotImplementedException();
        }

        public bool DoesFilePathContainPathEnvironmentVariable()
        {
            throw new System.NotImplementedException();
        }

        public bool DoesFilePathContainPathEnvironmentVariable(string variableName)
        {
            throw new System.NotImplementedException();
        }
    }
}