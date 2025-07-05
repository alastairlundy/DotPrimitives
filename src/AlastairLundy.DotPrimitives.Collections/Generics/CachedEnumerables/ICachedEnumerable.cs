/*
    AlastairLundy.DotPrimitives.Collections
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System.Collections.Generic;

// ReSharper disable TypeParameterCanBeVariant

namespace AlastairLundy.DotPrimitives.Collections.Generics.CachedEnumerables;

/// <summary>
/// Defines an interface for an Enumerable that can be cached and materialized on demand.
/// </summary>
/// <typeparam name="T">The type of elements in the Enumerable.</typeparam>
public interface ICachedEnumerable<T> : IEnumerable<T>
{
    /// <summary>
    /// Gets a list of the cached enumerable values.
    /// </summary>
    /// <returns>A list of the cached  values.</returns>
    IList<T> Cache { get; }

    /// <summary>
    /// Indicates whether the underlying data has been materialized (i.e. populated with actual data).
    /// </summary>
    /// <returns>True if the data has been materialized, false otherwise.</returns>
    bool HasBeenMaterialized { get; }

    /// <summary>
    /// Gets the materialization mode used by this enumeration.
    /// </summary>
    /// <returns>The materialization mode (e.g. Instant or Lazy).</returns>
    EnumerableMaterializationMode MaterializationMode { get; }

    /// <summary>
    /// Requests that the underlying data be materialized from its source if it has not already been materialized.
    /// This method triggers potential materialization of the enumeration values.
    /// </summary>
    void RequestMaterialization();
}