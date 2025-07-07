using System;
using System.Linq;
using AlastairLundy.DotPrimitives.Internal;

namespace AlastairLundy.DotPrimitives.IO.Permissions.Notations;

/// <summary>
/// 
/// </summary>
public struct NumericPermissionNotation : IUnixFilePermissionNotation,
    IEquatable<NumericPermissionNotation>
{
    /// <summary>
    /// 
    /// </summary>
    public UnixFilePermission UserPermissions { get; private set; }
    
    /// <summary>
    /// 
    /// </summary>
    public UnixFilePermission GroupPermissions { get; private set; }
    
    /// <summary>
    /// 
    /// </summary>
    public UnixFilePermission OthersPermissions { get; private set; }


    public NumericPermissionNotation(UnixFilePermission userPermissions,
        UnixFilePermission groupPermissions,
        UnixFilePermission othersPermissions)
    {
        UserPermissions = userPermissions;
        GroupPermissions = groupPermissions;
        OthersPermissions = othersPermissions;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static NumericPermissionNotation Parse(string input)
    {
        if (IsValidNotation(input) == false)
            throw new ArgumentException(Resources.Exceptions_Permissions_Unix_InvalidNumericNotation);

        if(input.StartsWith("0") == false)
            input =  input.Remove(0, 1);
        
        int user = int.Parse(input.First().ToString());
        int group = int.Parse(input[^2].ToString());
        int others = int.Parse(input.Last().ToString());
        
       UnixFilePermission userPermissions = user switch
        {
            0 => UnixFilePermission.None,
            1 => UnixFilePermission.UserExecute,
            2 => UnixFilePermission.UserWrite,
            3 => UnixFilePermission.UserWrite | UnixFilePermission.UserExecute,
            4 => UnixFilePermission.UserRead,
            5 => UnixFilePermission.UserRead | UnixFilePermission.UserExecute,
            6 => UnixFilePermission.UserRead | UnixFilePermission.UserWrite,
            7 => UnixFilePermission.UserRead | UnixFilePermission.UserWrite | UnixFilePermission.UserExecute,
            _ => throw new ArgumentException(Resources.Exceptions_Permissions_Unix_InvalidNumericNotation)
        };
        
        UnixFilePermission groupPermissions =  group switch
        {
            0 => UnixFilePermission.None,
            1 => UnixFilePermission.GroupExecute,
            2 => UnixFilePermission.GroupWrite,
            3 => UnixFilePermission.GroupWrite | UnixFilePermission.GroupExecute,
            4 => UnixFilePermission.GroupRead,
            5 => UnixFilePermission.GroupRead | UnixFilePermission.GroupExecute,
            6 => UnixFilePermission.GroupRead | UnixFilePermission.GroupWrite,
            7 => UnixFilePermission.GroupRead | UnixFilePermission.GroupWrite | UnixFilePermission.GroupExecute,
            _ => throw new ArgumentException(Resources.Exceptions_Permissions_Unix_InvalidNumericNotation)
        };
        
        UnixFilePermission othersPermissions = others switch
        {
            0 => UnixFilePermission.None,
            1 => UnixFilePermission.OtherExecute,
            2 => UnixFilePermission.OtherWrite,
            3 => UnixFilePermission.OtherWrite | UnixFilePermission.OtherExecute,
            4 => UnixFilePermission.OtherRead,
            5 => UnixFilePermission.OtherRead | UnixFilePermission.OtherExecute, 6 => UnixFilePermission.OtherRead | UnixFilePermission.OtherWrite,
            7 => UnixFilePermission.OtherRead | UnixFilePermission.OtherWrite | UnixFilePermission.OtherExecute,
            _ => throw new ArgumentException(Resources.Exceptions_Permissions_Unix_InvalidNumericNotation)
        };

        return new NumericPermissionNotation(userPermissions, groupPermissions, othersPermissions);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="input"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public static bool TryParse(string input, out NumericPermissionNotation? result)
    {
        try
        {
            result = Parse(input);
            return true;
        }
        catch
        {
            result = null;
            return false;
        }
    }
    
    private static bool IsValidNotation(string notation)
    {
        if (notation.Length is >= 3 and <= 4 || int.TryParse(notation, out int result) == false) 
            return false;

        if (notation.Length == 4 && notation[0] != '0')
            return result is >= 0 and <= 4777 && notation.ToCharArray()
                .All(x => x != '8' && x != '9');
        else
            return result is >= 0 and <= 777 && notation.Length is >= 3 and <= 4;
    }

    public bool Equals(NumericPermissionNotation other)
    {
        return UserPermissions == other.UserPermissions &&
               GroupPermissions == other.GroupPermissions &&
               OthersPermissions == other.OthersPermissions;
    }

    public override bool Equals(object? obj)
    {
        if(obj is null)
            return false;
        
        return obj is NumericPermissionNotation other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine((int)UserPermissions, (int)GroupPermissions, (int)OthersPermissions);
    }

    public static bool operator ==(NumericPermissionNotation left, NumericPermissionNotation right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(NumericPermissionNotation left, NumericPermissionNotation right)
    {
        return left.Equals(right) == false;
    }
}