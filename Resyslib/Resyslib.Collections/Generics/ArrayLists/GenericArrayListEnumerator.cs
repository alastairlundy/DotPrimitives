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

namespace AlastairLundy.Resyslib.Collections.Generics.ArrayLists
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct GenericArrayListEnumerator<T> : IEnumerator<T>
    {
        private int _position = -1;
    
        private readonly GenericArrayList<T> _list;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list">The list to enumerate.</param>
        public GenericArrayListEnumerator(GenericArrayList<T> list)
        {
            _list = list;
        }
    
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool MoveNext()
        {
            _position++;
        
            return (_position < _list.Count);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Reset()
        {
            _position = -1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="Exception"></exception>
        public T Current => _list[_position];

        /// <summary>
        /// 
        /// </summary>
        object? IEnumerator.Current => Current;

        /// <summary>
        /// Releases any resources used by this instance of the enumerator.
        /// </summary>
        public void Dispose()
        {
            Reset();
        }
    }
}