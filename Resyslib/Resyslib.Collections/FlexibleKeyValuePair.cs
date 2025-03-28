using System;
using System.Collections.Generic;

// ReSharper disable ClassNeverInstantiated.Global

// ReSharper disable NonReadonlyMemberInGetHashCode
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable MemberCanBeProtected.Global

namespace AlastairLundy.Resyslib.Collections;

/// <summary>
/// A Key and Value combination, where the Key must stay the same but the value can change.
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TValue"></typeparam>
public class FlexibleKeyValuePair<TKey, TValue> : IEquatable<FlexibleKeyValuePair<TKey, TValue>>
{
    /// <summary>
    /// 
    /// </summary>
    public TKey Key { get; protected set; }
    
    /// <summary>
    /// 
    /// </summary>
    public TValue Value { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public FlexibleKeyValuePair(TKey key, TValue value)
    {
        Key = key;
        Value = value;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(FlexibleKeyValuePair<TKey, TValue>? other)
    {
        if (other is null)
        {
            return false;
        }
        
        return EqualityComparer<TKey>.Default.Equals(Key, other.Key)
               && EqualityComparer<TValue>.Default.Equals(Value, other.Value);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (obj is FlexibleKeyValuePair<TKey, TValue> other)
        {
            return Equals(other);
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
       return HashCode.Combine(Key, Value);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool Equals(FlexibleKeyValuePair<TKey, TValue>? left, FlexibleKeyValuePair<TKey, TValue>? right)
    {
        if (left is null || right is null)
        {
            return false;
        }
        
        return left.Equals(right);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator ==(FlexibleKeyValuePair<TKey, TValue>? left, FlexibleKeyValuePair<TKey, TValue>? right)
    {
        return Equals(left, right);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator !=(FlexibleKeyValuePair<TKey, TValue>? left, FlexibleKeyValuePair<TKey, TValue>? right)
    {
        return Equals(left, right) == false;
    }
}

