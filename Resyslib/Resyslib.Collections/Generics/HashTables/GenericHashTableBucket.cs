/*
    Resyslib.Collections
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using AlastairLundy.Resyslib.Collections.Generics.ArrayLists;

using AlastairLundy.Resyslib.Collections.Internal.Localizations;

namespace AlastairLundy.Resyslib.Collections.Generics.HashTables
{
    internal struct GenericHashTableBucket<TKey, TValue>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <remarks> <see cref="GenericHashTable{TKey,TValue}"/> uses the first half of a <see cref="TKey"/>'s hashcode for creating bucket IDs,
        /// and grouping Key Value Pairs in the same bucket.</remarks>
        public int BucketId { get; }

    
        public int Size => Items.Count;

        public GenericHashTableBucket(int bucketCode)
        {
            BucketId = bucketId;
            _items = new GenericArrayList<FlexibleKeyValuePair<TKey, TValue>>();
        }

        internal void Add(FlexibleKeyValuePair<TKey, TValue> item)
        {
            _items.Add(item);
        }

        internal void Remove(FlexibleKeyValuePair<TKey, TValue> item)
        {
            _items.Remove(item);
        }

        public GenericArrayList<FlexibleKeyValuePair<TKey, TValue>> Items => _items;
    
        private readonly GenericArrayList<FlexibleKeyValuePair<TKey, TValue>> _items;
    }
}