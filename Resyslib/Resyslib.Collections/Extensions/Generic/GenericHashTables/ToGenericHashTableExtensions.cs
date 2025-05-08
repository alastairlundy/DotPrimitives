/*
    Resyslib.Collections
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections;

using AlastairLundy.Resyslib.Collections.Generics.HashTables;

namespace AlastairLundy.Resyslib.Collections.Extensions.Generic.GenericHashTables
{
    /// <summary>
    /// 
    /// </summary>
    public static class ToGenericHashTableExtensions
    {
        /// <summary>
        /// Converts a Hashtable to a GenericHashTable that supports generics.
        /// </summary>
        /// <param name="hashTable">The Hashtable to convert.</param>
        /// <typeparam name="TKey">The type of Keys the Hashtable stores.</typeparam>
        /// <typeparam name="TValue">The type of Values the Hashtable stores.</typeparam>
        /// <returns>A new GenericHashTable with the items from the Hashtable.</returns>
        /// <exception cref="ArgumentException">Thrown if the type of Keys or Values specified are not used by the HashTable.</exception>
        public static GenericHashTable<TKey, TValue> ToGenericHashTable<TKey, TValue>(this Hashtable hashTable)
        {
            GenericHashTable<TKey, TValue> output = new();

            foreach (DictionaryEntry entry in hashTable)
            {
                if (entry.Key is TKey key && entry.Value is TValue value)
                {
                    output.Add(key, value);
                }
                else if(entry.Key is TKey)
                {
                    throw new ArgumentException(
                            $"TValue type specified of {typeof(TValue)} does not match the type of Values stored in the Hashtable {nameof(hashTable)}");
                }
                else if (entry.Value is TValue)
                {
                    throw new ArgumentException(
                        $"TKey type specified of {typeof(TKey)} does not match the type of Keys stored in the Hashtable {nameof(hashTable)}");   
                }
            }
            
            return output;
        }
    }   
}