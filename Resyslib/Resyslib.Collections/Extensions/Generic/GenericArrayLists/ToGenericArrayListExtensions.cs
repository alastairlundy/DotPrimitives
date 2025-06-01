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
using AlastairLundy.Resyslib.Collections.Generics.ArrayLists;

// ReSharper disable SuggestVarOrType_BuiltInTypes
// ReSharper disable MemberCanBePrivate.Global

namespace AlastairLundy.Resyslib.Collections.Extensions.Generic.GenericArrayLists
{
    public static class ToGenericArrayListExtensions
    {
        /// <summary>
        /// Converts an ArrayList to a GenericArrayList that supports generics.
        /// </summary>
        /// <param name="arrayList">The arraylist to convert.</param>
        /// <typeparam name="T">The type of Type the ArrayList stores.</typeparam>
        /// <returns>A new GenericArrayList of type T with the items from the ArrayList.</returns>
        /// <exception cref="ArgumentException">Thrown if the type specified is not the type stored in the ArrayList.</exception>
        public static GenericArrayList<T> ToGenericArrayList<T>(this ArrayList arrayList)
        {
            if (typeof(T) != arrayList.GetType())
            {
                throw new ArgumentException(
                    $"Type specified of {typeof(T)} does not match array list of type {arrayList.GetType()}.");
            }

            GenericArrayList<T> output = new();

            foreach (object obj in arrayList)
            {
                if (obj is T t)
                {
                    output.Add(t);
                }
            }

            return output;
        }

        /// <summary>
        /// Converts an ArrayList to an IGenericArrayList that supports generics.
        /// </summary>
        /// <param name="arrayList">The arraylist to convert.</param>
        /// <typeparam name="T">The type of Type the ArrayList stores.</typeparam>
        /// <returns>A new IGenericArrayList of type T with the items from the ArrayList.</returns>
        public static IGenericArrayList<T> ToIGenericArrayList<T>(this ArrayList arrayList)
        {
            return ToGenericArrayList<T>(arrayList);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enumerable"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static GenericArrayList<T> ToGenericArrayList<T>(this IEnumerable<T> enumerable)
        {
            GenericArrayList<T> output;
            
            if (enumerable is ICollection<T> collection)
            {
               output = new GenericArrayList<T>(capacity: collection.Count);
               output.AddRange(collection);
            }
            else
            {
                output = new GenericArrayList<T>();
                output.AddRange(enumerable);
            }

            
            return output;
        }
    }
}