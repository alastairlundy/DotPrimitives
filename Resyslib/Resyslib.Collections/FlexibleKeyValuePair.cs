/*
    Resyslib.Collections
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

using AlastairLundy.Resyslib.Collections.Internal.Localizations;

// ReSharper disable ClassNeverInstantiated.Global

// ReSharper disable NonReadonlyMemberInGetHashCode
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable MemberCanBeProtected.Global

namespace AlastairLundy.Resyslib.Collections
{
    /// <summary>
    /// A Key and Value combination, where the Key must stay the same but the value can change.
    /// </summary>
    /// <typeparam name="TKey">The key for this flexible key-value pair.</typeparam>
    /// <typeparam name="TValue">The value for this flexible key-value pair.</typeparam>
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
        /// 
        /// </summary>
        /// <param name="newValue"></param>
        public void ReplaceValue(TValue newValue)
        {
            Value = newValue;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current FlexibleKeyValuePair. </summary>
        /// <param name="other">The other flexible key-value pair to compare with.</param>
        /// <returns>True if the flexible key-value pairs are equal; otherwise, false.</returns>
        public bool Equals(FlexibleKeyValuePair<TKey, TValue> other)
        {
            if (Key is null || Value is null)
            {
                throw new NullReferenceException();
            }
            
            return Key.Equals(other.Key) && Value.Equals(other.Value);
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
        /// Creates a new KeyValuePair from this FlexibleKeyValuePair.
        /// </summary>
        /// <returns>A newly created KeyValuePair with the key and value from this FlexibleKeyValuePair.</returns>
        [Pure]
        public KeyValuePair<TKey, TValue> ToKeyValuePair()
        {
            return new KeyValuePair<TKey, TValue>(Key, Value);
        }

        /// <summary>
        /// Returns a string representation of the Flexible Key Value Pair.
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
        /// Creates a new instance of the FlexibleKeyValuePair class from the specified KeyValuePair. </summary>
        /// <param name="pair">The key-value pair to create a flexible key-value pair from.</param>
        /// <typeparam name="TKey">The type of the key in the flexible key-value pair.</typeparam>
        /// <typeparam name="TValue">The type of the value in the flexible key-value pair.</typeparam>
        /// <returns>A new instance of the FlexibleKeyValuePair class containing the specified key and value.</returns>
        [Pure]
        public static FlexibleKeyValuePair<TKey, TValue> FromKeyValuePair<TKey, TValue>(KeyValuePair<TKey, TValue> pair)
        {
            return new FlexibleKeyValuePair<TKey, TValue>(pair.Key, pair.Value);
        }
    }
}
