/*
    Resyslib.Collections
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System.Collections.Generic;
using System.Linq;

using AlastairLundy.Resyslib.Collections.Generics.HashTables;

namespace AlastairLundy.Resyslib.Collections.Extensions.Generic.GenericHashTables
{
    public static class GenericHashTableToExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hashTable"></param>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<TKey, TValue>> AsEnumerable<TKey, TValue>(
            this IGenericHashTable<TKey, TValue> hashTable)
        {
            return from item in hashTable
                select item;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hashTable"></param>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(
            this IGenericHashTable<TKey, TValue> hashTable) where TKey : notnull
        {
            Dictionary<TKey, TValue> output = new Dictionary<TKey, TValue>();

            foreach (KeyValuePair<TKey, TValue> pair in hashTable)
            {
                output.Add(pair.Key, pair.Value);
            }
            
            return output;
        }
    }
}

