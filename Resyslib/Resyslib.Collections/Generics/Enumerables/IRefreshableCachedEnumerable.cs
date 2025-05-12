/*
    Resyslib.Collections
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */


using System.Collections.Generic;

namespace AlastairLundy.Resyslib.Collections.Generics.Enumerables
{ 
    
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRefreshableCachedEnumerable<T> : ICachedEnumerable<T>
    {
        
        /// <summary>
        /// 
        /// </summary>
        void RefreshCache(IEnumerable<T> source);
    }
    
}