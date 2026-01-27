/*
    MIT License

    Copyright (c) 2025-2026 Alastair Lundy

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.
 */

// ReSharper disable TypeParameterCanBeVariant

namespace DotPrimitives.Collections.Enumerables.Cached;

/// <summary>
/// Defines an interface for an Enumerable that can be cached and materialized on demand.
/// </summary>
/// <typeparam name="T">The type of elements in the Enumerable.</typeparam>
public interface ICachedEnumerable<T> : IEnumerable<T>
{
    /// <summary>
    /// Indicates whether the underlying data has been materialized (i.e. populated with actual data).
    /// </summary>
    /// <returns>True if the data has been materialized, false otherwise.</returns>
    bool HasBeenMaterialized { get; }

    /// <summary>
    /// Determines whether the <see cref="CachedEnumerable{T}"/> is empty without materializing the source.
    /// </summary>
    /// <remarks>May return false if the source type is an <see cref="IEnumerable{T}"/> and it hasn't yet been materialized.</remarks>
    bool IsEmpty { get; }
    
    /// <summary>
    /// Gets the materialization mode used by this enumeration.
    /// </summary>
    /// <returns>The materialization mode (e.g. Instant or Lazy).</returns>
    EnumerableMaterializationMode MaterializationMode { get; }
}