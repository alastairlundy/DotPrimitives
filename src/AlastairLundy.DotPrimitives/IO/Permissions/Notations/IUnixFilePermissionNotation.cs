using System.IO;

namespace AlastairLundy.DotPrimitives.IO.Permissions.Notations;

/// <summary>
/// 
/// </summary>
public interface IUnixFilePermissionNotation
{
    /// <summary>
    /// 
    /// </summary>
    public UnixFileMode UserPermissions { get; }
    
    /// <summary>
    /// 
    /// </summary>
    public UnixFileMode GroupPermissions { get; }
    
    /// <summary>
    /// 
    /// </summary>
    public UnixFileMode OthersPermissions { get; }
}