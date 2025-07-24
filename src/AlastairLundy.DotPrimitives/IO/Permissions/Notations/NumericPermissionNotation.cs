/*
    AlastairLundy.DotPrimitives
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;
using System.IO;
using System.Linq;

using AlastairLundy.DotPrimitives.Internals.Localizations;

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
    public UnixFileMode UserPermissions { get; private set; }
    
    /// <summary>
    /// 
    /// </summary>
    public UnixFileMode GroupPermissions { get; private set; }
    
    /// <summary>
    /// 
    /// </summary>
    public UnixFileMode OthersPermissions { get; private set; }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="userPermissions"></param>
    /// <param name="groupPermissions"></param>
    /// <param name="othersPermissions"></param>
    public NumericPermissionNotation(UnixFileMode userPermissions,
        UnixFileMode groupPermissions,
        UnixFileMode othersPermissions)
    {
        UserPermissions = userPermissions;
        GroupPermissions = groupPermissions;
        OthersPermissions = othersPermissions;
    }
    
    /// <summary>
    /// Parses a 
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static NumericPermissionNotation Parse(string input)
    {
#if NET8_0_OR_GREATER
        ArgumentException.ThrowIfNullOrEmpty(input,  nameof(input));
#endif
        
        if (IsValidNotation(input) == false)
            throw new ArgumentException(Resources.Exceptions_Permissions_Unix_InvalidNumericNotation);

        if(input.StartsWith("0") == false)
            input =  input.Remove(0, 1);
        
        int user = int.Parse(input.First().ToString());
        int group = int.Parse(input[^2].ToString());
        int others = int.Parse(input.Last().ToString());
        
       UnixFileMode userPermissions = user switch
        {
            0 => UnixFileMode.None,
            1 => UnixFileMode.UserExecute,
            2 => UnixFileMode.UserWrite,
            3 => UnixFileMode.UserWrite | UnixFileMode.UserExecute,
            4 => UnixFileMode.UserRead,
            5 => UnixFileMode.UserRead | UnixFileMode.UserExecute,
            6 => UnixFileMode.UserRead | UnixFileMode.UserWrite,
            7 => UnixFileMode.UserRead | UnixFileMode.UserWrite | UnixFileMode.UserExecute,
            _ => throw new ArgumentException(Resources.Exceptions_Permissions_Unix_InvalidNumericNotation)
        };
        
        UnixFileMode groupPermissions =  group switch
        {
            0 => UnixFileMode.None,
            1 => UnixFileMode.GroupExecute,
            2 => UnixFileMode.GroupWrite,
            3 => UnixFileMode.GroupWrite | UnixFileMode.GroupExecute,
            4 => UnixFileMode.GroupRead,
            5 => UnixFileMode.GroupRead | UnixFileMode.GroupExecute,
            6 => UnixFileMode.GroupRead | UnixFileMode.GroupWrite,
            7 => UnixFileMode.GroupRead | UnixFileMode.GroupWrite | UnixFileMode.GroupExecute,
            _ => throw new ArgumentException(Resources.Exceptions_Permissions_Unix_InvalidNumericNotation)
        };
        
        UnixFileMode othersPermissions = others switch
        {
            0 => UnixFileMode.None,
            1 => UnixFileMode.OtherExecute,
            2 => UnixFileMode.OtherWrite,
            3 => UnixFileMode.OtherWrite | UnixFileMode.OtherExecute,
            4 => UnixFileMode.OtherRead,
            5 => UnixFileMode.OtherRead | UnixFileMode.OtherExecute, 6 => UnixFileMode.OtherRead | UnixFileMode.OtherWrite,
            7 => UnixFileMode.OtherRead | UnixFileMode.OtherWrite | UnixFileMode.OtherExecute,
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(NumericPermissionNotation other)
    {
        return UserPermissions == other.UserPermissions &&
               GroupPermissions == other.GroupPermissions &&
               OthersPermissions == other.OthersPermissions;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object? obj)
    {
        if(obj is null)
            return false;
        
        return obj is NumericPermissionNotation other && Equals(other);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        return HashCode.Combine((int)UserPermissions, 
            (int)GroupPermissions, (int)OthersPermissions);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator ==(NumericPermissionNotation left, NumericPermissionNotation right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator !=(NumericPermissionNotation left, NumericPermissionNotation right)
    {
        return left.Equals(right) == false;
    }
}