/*
    Resyslib.Collections
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System.Collections;
using System.Collections.Generic;

namespace AlastairLundy.Resyslib.Collections.Generics.Enumerables;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public class RefreshableCachedEnumerable<T> : IRefreshableCachedEnumerable<T>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="enumerable"></param>
    /// <param name="mode"></param>
    public RefreshableCachedEnumerable(IEnumerable<T> enumerable,
        EnumerableMaterializationMode mode = EnumerableMaterializationMode.Instant)
    {
       _cache = new List<T>();

       Source = enumerable;
       MaterializationMode = mode;
       
       RefreshCache(Source);
    }

    private IEnumerable<T> Source { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    public void RefreshCache(IEnumerable<T> source)
    {
        HasBeenMaterialized = false;

        switch (MaterializationMode)
        {
            case EnumerableMaterializationMode.Instant:
                _cache.Clear();
                RequestMaterialization();
                break;
            case EnumerableMaterializationMode.Lazy:
                break;
            default:
                break;
        }
    }

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

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    
    private readonly List<T> _cache;
    
    public IList<T> Cache => _cache;
    
    public bool HasBeenMaterialized { get; private set; }
    public EnumerableMaterializationMode MaterializationMode { get; }

    public void RequestMaterialization()
    {
        if (HasBeenMaterialized == false)
        {
            foreach (T item in Source)
            {
                _cache.Add(item);
            }

            HasBeenMaterialized = true;
        }
    }
}