/*
    AlastairLundy.DotPrimitives.Collections
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System.Collections.Generic;

using AlastairLundy.DotPrimitives.Collections.Enumerables.Cached;

namespace AlastairLundy.DotPrimitives.Collections.Extensions;

public static class EnumerableToCachedExtensions
{
   /// <summary>
   /// 
   /// </summary>
   /// <param name="source"></param>
   /// <param name="materializationMode"></param>
   /// <typeparam name="T"></typeparam>
   /// <returns></returns>
   public static CachedEnumerable<T> Cache<T>(this IEnumerable<T> source,
      EnumerableMaterializationMode materializationMode = EnumerableMaterializationMode.Lazy)
   {
      return new CachedEnumerable<T>(source, materializationMode);
   }
}