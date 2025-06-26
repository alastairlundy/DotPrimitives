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

namespace AlastairLundy.Resyslib.Collections.Generics.ArrayLists;

/// <summary>
/// An enumerator for a GenericArrayList that provides access to its elements.
/// </summary>
/// <typeparam name="T">The type of elements in the GenericArrayList.</typeparam>
[Obsolete(Deprecations.DeprecationMessages.DeprecationV2)]
public struct GenericArrayListEnumerator<T> : IEnumerator<T>
{
    private int _position = -1;
    
    private readonly GenericArrayList<T> _list;
        
    /// <summary>
    /// Initializes a new instance of the Enumerator with the GenericArrayList to enumerate.
    /// </summary>
    /// <param name="list">The list to enumerate.</param>
    public GenericArrayListEnumerator(GenericArrayList<T> list)
    {
        _list = list;
    }
    
    /// <summary>
    /// Advances the enumerator to the next position in the sequence.
    /// If the current position is a valid position for this instance of the enumerator,
    /// it moves the position to the next item and returns true.
    /// </summary>
    /// <returns>True if there are more items to enumerate; otherwise, false.</returns>
    public bool MoveNext()
    {
        _position++;
        
        return (_position < _list.Count);
    }

    /// <summary>
    /// Resets the position of this enumerator back to its initial value.
    /// </summary>
    public void Reset()
    {
        _position = -1;
    }

    /// <summary>
    /// Gets the current item in the sequence.
    /// </summary>
    public T Current => _list[_position];

    /// <summary>
    /// Implement the IEquatable interface to allow for equality comparisons between enumerators.
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