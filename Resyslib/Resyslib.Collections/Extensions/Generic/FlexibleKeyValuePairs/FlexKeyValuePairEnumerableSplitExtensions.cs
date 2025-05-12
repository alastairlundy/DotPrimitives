/*
    Resyslib.Collections
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System.Collections.Generic;
using System.Linq;

namespace AlastairLundy.Resyslib.Collections.Extensions.Generic.FlexibleKeyValuePairs
{
    public static class FlexKeyValuePairEnumerableSplitExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pairs"></param>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static IEnumerable<TKey> ToKeys<TKey, TValue>(this IEnumerable<FlexibleKeyValuePair<TKey, TValue>> pairs)
        {
            return pairs.Select(pair => pair.Key);
        }
    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pairs"></param>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static IEnumerable<TValue> ToValues<TKey, TValue>(this IEnumerable<FlexibleKeyValuePair<TKey, TValue>> pairs)
        {
            return pairs.Select(pair => pair.Value);
        }
    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pairs"></param>
        /// <param name="keys"></param>
        /// <param name="values"></param>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        public static void ToSplitPairs<TKey, TValue>(this IEnumerable<FlexibleKeyValuePair<TKey, TValue>> pairs, out IEnumerable<TKey> keys, out IEnumerable<TValue> values)
        {
            List<TKey> outputKeys = new List<TKey>();
            List<TValue> outputValues = new List<TValue>();

            foreach (FlexibleKeyValuePair<TKey, TValue> pair in pairs)
            {
                outputKeys.Add(pair.Key);
                outputValues.Add(pair.Value);
            }
        
            keys = outputKeys;
            values = outputValues;
        }
    }
}