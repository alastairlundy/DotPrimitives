/*
    AlastairLundy.DotPrimitives.Collections
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

namespace AlastairLundy.DotPrimitives.Collections.CachedEnumerables;

/// <summary>
/// Specifies the materialization mode for Cache-able enumerables.
/// </summary>
public enum EnumerableMaterializationMode
{
    /// <summary>
    /// Instantiates the enumeration values at compile-time,
    /// providing a more efficient and predictable experience.
    /// </summary>
    Instant,
    /// <summary>
    /// Delays the instantiation of enumeration values until they are actually requested,
    /// potentially reducing memory usage.
    /// </summary>
    Lazy
}