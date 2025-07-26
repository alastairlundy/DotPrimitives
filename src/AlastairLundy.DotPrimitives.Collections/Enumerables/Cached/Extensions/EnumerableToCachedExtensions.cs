/*
    AlastairLundy.DotPrimitives.Collections
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System.Collections.Generic;

namespace AlastairLundy.DotPrimitives.Collections.Enumerables.Cached;

/// <summary>
/// 
/// </summary>
public static class EnumerableToCachedExtensions
{
   /// <summary>
   /// 
   /// </summary>
   /// <param name="source">The underlying enumerable data to be cached.</param>
   /// <param name="mode">The desired level of materialization for the cached values,
   /// defaults to Lazy if not provided.
   /// </param>
   /// <typeparam name="T">The type of elements stored in the CachedEnumerable.</typeparam>
   /// <returns>An instantiated <see cref="CachedEnumerable{T}"/> with the specified source <see cref="IEnumerable{T}"/> and <see cref="EnumerableMaterializationMode"/>.</returns>
   public static CachedEnumerable<T> Cache<T>(this IEnumerable<T> source,
      EnumerableMaterializationMode mode = EnumerableMaterializationMode.Lazy)
   {
      return new CachedEnumerable<T>(source, mode);
   }
   }
}