/*
    AlastairLundy.DotPrimitives.Collections
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System.Collections.Generic;

namespace AlastairLundy.DotPrimitives.Collections.Groupings;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TElement"></typeparam>
public interface IGroupingList<out TKey, TElement>  : IGroupingCollection<TKey, TElement>, IList<TElement>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    new int IndexOf(TElement item);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    /// <param name="item"></param>
    new void Insert(int index, TElement item);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    new void RemoveAt(int index);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    new TElement this[int index] { get; set; }
}