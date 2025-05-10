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

namespace AlastairLundy.Resyslib.Collections.Generics.Caching
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CachedEnumerable<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> _source;
    
        private bool HasBeenMaterialized { get; set; }

        private readonly List<T> _cache;
    
        /// <summary>
        /// 
        /// </summary>
        public EnumerableMaterializationMode MaterializationMode { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="materializationPreference"></param>
        public CachedEnumerable(IEnumerable<T> source,
            EnumerableMaterializationMode materializationPreference =
                EnumerableMaterializationMode.Instant)
        {
            _source = source;
            _cache = new List<T>();

            MaterializationMode = materializationPreference;
            HasBeenMaterialized = false;
        
            if (materializationPreference == EnumerableMaterializationMode.Instant)
            {
                MaterializeEnumerable();
            }
        }

        private void MaterializeEnumerable()
        {
            foreach (T item in _source)
            {
                _cache.Add(item);
            }
            
            HasBeenMaterialized = true;
        }
    
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            if (MaterializationMode == EnumerableMaterializationMode.Instant)
            {
                for (int i = 0; i < _cache.Count; i++)
                {
                    yield return _cache[i];
                }
            }
            else
            {
                if (HasBeenMaterialized == false)
                {
                    MaterializeEnumerable();
                }
            
                foreach (T item in _cache)
                {
                    yield return item;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}