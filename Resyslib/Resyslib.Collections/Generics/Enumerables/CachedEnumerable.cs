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

namespace AlastairLundy.Resyslib.Collections.Generics.Enumerables
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CachedEnumerable<T> : ICachedEnumerable<T>, IDisposable
    {
        private readonly IEnumerable<T> _source;

        /// <summary>
        /// The cache.
        /// </summary>
        /// <remarks>Accessing the Cache will materialize the cache if the Cache has not already been materialized.
        /// <para>Accessing the Cache prematurely may be computationally expensive.</para>
        /// </remarks>
        public IList<T> Cache
        {
            get
            {
                if (HasBeenMaterialized == false)
                {
                    RequestMaterialization();
                }
                
                return _cache;
            }
        }
        public bool HasBeenMaterialized { get; private set; }

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

            switch (materializationPreference)
            {
                case EnumerableMaterializationMode.Instant:
                    RequestMaterialization();
                    break;
                case EnumerableMaterializationMode.Lazy:
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void RequestMaterialization()
        {
            if (HasBeenMaterialized == false)
            {
                foreach (T item in _source)
                {
                    _cache.Add(item);
                }
                
                HasBeenMaterialized = true;
            }
        }
    
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            if (MaterializationMode == EnumerableMaterializationMode.Lazy)
            {
                if (HasBeenMaterialized == false)
                {
                    RequestMaterialization();
                }
            }

            foreach (T item in _cache)
            {
                yield return item;
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

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            _cache.Clear();
        }
    }
}