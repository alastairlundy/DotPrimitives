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
using System.Diagnostics.Contracts;

using AlastairLundy.Resyslib.Collections.Internal.Localizations;

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
public struct FlexibleKeyValuePair<TKey, TValue> : IEquatable<FlexibleKeyValuePair<TKey, TValue>>
{
    /// <summary>
    /// Gets the key of this FlexibleKeyValuePair.
    /// </summary>
    public TKey Key { get; }
    
    /// <summary>
    /// Gets or sets the value of this FlexibleKeyValuePair. </summary>
    public TValue Value { get; set; }


    /// <summary>
    /// Initializes a new instance of the FlexibleKeyValuePair class with the specified key and value. </summary>
    /// <param name="key">The key for this flexible key-value pair.</param>
    /// <param name="value">The value for this flexible key-value pair.</param>
    public FlexibleKeyValuePair(TKey key, TValue value)
    {
        Key = key;
        Value = value;
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current FlexibleKeyValuePair. </summary>
    /// <param name="other">The other flexible key-value pair to compare with.</param>
    /// <returns>True if the flexible key-value pairs are equal; otherwise, false.</returns>
    public bool Equals(FlexibleKeyValuePair<TKey, TValue>? other)
    {
        if (other is null)
        {
            return false;
        }
        
        return EqualityComparer<TKey>.Default.Equals(Key, other.Value.Key)
               && EqualityComparer<TValue>.Default.Equals(Value, other.Value.Value);
    }

    /// <summary>
    /// Compares the current object with another object to determine whether they represent the same key-value pair. </summary>
    /// <param name="obj">The object to compare with this FlexibleKeyValuePair.</param>
    /// <returns>True if the objects are equal; otherwise, false.</returns>
    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }
        
        return obj is FlexibleKeyValuePair<TKey, TValue> other && Equals(other);
    }
    
    /// <summary>
    /// Returns a hash code for this instance. </summary>
    /// <returns>A hash code value representing the current object.</returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(Key, Value);
    }
    
    /// <summary>
    /// Determines whether the specified object is equal to the current FlexibleKeyValuePair. </summary>
    /// <param name="left">The first flexible key-value pair to compare.</param>
    /// <param name="right">The second flexible key-value pair to compare.</param>
    /// <returns>True if the flexible key-value pairs are equal; otherwise, false.</returns>
    public static bool operator ==(FlexibleKeyValuePair<TKey, TValue>? left, FlexibleKeyValuePair<TKey, TValue>? right)
    {
        return Equals(left, right);
    }

    /// <summary>
    /// Determines whether an object is not equal to the current FlexibleKeyValuePair. </summary>
    /// <param name="left">The first flexible key-value pair to compare.</param>
    /// <param name="right">The second flexible key-value pair to compare.</param>
    /// <returns>True if the flexible key-value pairs are not equal; otherwise, false.</returns>
    public static bool operator !=(FlexibleKeyValuePair<TKey, TValue>? left, FlexibleKeyValuePair<TKey, TValue>? right)
    {
        return Equals(left, right) == false;
    }

    /// <summary>
    /// Compares the current FlexibleKeyValuePair with another FlexibleKeyValuePair. </summary>
    /// <param name="other">The other flexible key-value pair to compare with.</param>
    /// <returns>True if the flexible key-value pairs are equal; otherwise, false.</returns>
    public bool Equals(FlexibleKeyValuePair<TKey, TValue> other)
    {
        return EqualityComparer<TKey>.Default.Equals(Key, other.Key) &&
               EqualityComparer<TValue>.Default.Equals(Value, other.Value);
    }
    
    /// <summary>
    /// Creates a new KeyValuePair from this FlexibleKeyValuePair.
    /// </summary>
    /// <returns>A newly created KeyValuePair with the key and value from this FlexibleKeyValuePair.</returns>
    public KeyValuePair<TKey, TValue> ToKeyValuePair()
    {
        return new KeyValuePair<TKey, TValue>(Key, Value);
    }

    /// <summary>
    /// Returns a string representation of the 
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return $"{Resources.Labels_Key}: {Key}, {Resources.Labels_Value}: {Value}";
    }
}

/// <summary>
/// A static helper class that makes interoperability between FlexibleKeyValuePair and other types easier.
/// </summary>
public static class FlexibleKeyValuePair
{
    /// <summary>
    /// Compares two objects to determine whether they represent the same key-value pair. </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns>True if the objects are equal; otherwise, false.</returns>
    [Pure]
    public static bool Equals<TKey, TValue>(FlexibleKeyValuePair<TKey, TValue>? left, FlexibleKeyValuePair<TKey, TValue>? right)
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
    /// <param name="pair"></param>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    [Pure]
    public static FlexibleKeyValuePair<TKey, TValue> FromKeyValuePair<TKey, TValue>(KeyValuePair<TKey, TValue> pair)
    {
        return new FlexibleKeyValuePair<TKey, TValue>(pair.Key, pair.Value);
    }
}
