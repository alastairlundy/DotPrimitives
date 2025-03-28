namespace AlastairLundy.Resyslib.Abstractions
{
    public interface IEnvironmentVariableResolver
    {
        char PathEnvironmentVariableSeparator { get; }
        
        bool DoesEnvironmentVariableExist(string variableName);
    
        string GetEnvironmentVariable(string variableName);
    
        bool DoesFilePathContainPathEnvironmentVariable(string filePath);
        bool DoesFilePathContainPathEnvironmentVariable(string filePath, string variableName);
    }
}