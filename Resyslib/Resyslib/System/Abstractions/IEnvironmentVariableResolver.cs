namespace Resyslib.Abstractions
{
    public interface IEnvironmentVariableResolver
    {
        bool DoesEnvironmentVariableExist(string variableName);
    
        string GetEnvironmentVariable(string variableName);
    
        bool DoesFilePathContainPathEnvironmentVariable();
        bool DoesFilePathContainPathEnvironmentVariable(string variableName);
    }
}