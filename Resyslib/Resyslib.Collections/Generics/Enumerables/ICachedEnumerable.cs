/*
    Resyslib.Collections
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System.Collections.Generic;

// ReSharper disable TypeParameterCanBeVariant

namespace AlastairLundy.Resyslib.Collections.Generics.Enumerables
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICachedEnumerable<T> : IEnumerable<T>
    {
        /// <summary>
        /// 
        /// </summary>
        IList<T> Cache { get; }

        /// <summary>
        /// 
        /// </summary>
        bool HasBeenMaterialized { get; }

        /// <summary>
        /// 
        /// </summary>
        EnumerableMaterializationMode MaterializationMode { get; }

        /// <summary>
        /// 
        /// </summary>
        void RequestMaterialization();
    }
}