namespace AlastairLundy.DotPrimitives.IO.Permissions.Notations;

/// <summary>
/// 
/// </summary>
public interface IUnixFilePermissionNotation
{
    /// <summary>
    /// 
    /// </summary>
    public UnixFilePermission UserPermissions { get; }
    
    /// <summary>
    /// 
    /// </summary>
    public UnixFilePermission GroupPermissions { get; }
    
    /// <summary>
    /// 
    /// </summary>
    public UnixFilePermission OthersPermissions { get; }
}