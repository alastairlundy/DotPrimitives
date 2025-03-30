/*
        MIT License
       
       Copyright (c) 2024-2025 Alastair Lundy
       
       Permission is hereby granted, free of charge, to any person obtaining a copy
       of this software and associated documentation files (the "Software"), to deal
       in the Software without restriction, including without limitation the rights
       to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
       copies of the Software, and to permit persons to whom the Software is
       furnished to do so, subject to the following conditions:
       
       The above copyright notice and this permission notice shall be included in all
       copies or substantial portions of the Software.
       
       THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
       IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
       FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
       AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
       LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
       OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
       SOFTWARE.
   */

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

