/*
    DotPrimitives.Collections
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;

using AlastairLundy.DotPrimitives.Collections.Internal.Localizations;

// ReSharper disable ConvertToPrimaryConstructor

namespace AlastairLundy.DotPrimitives.Collections.Exceptions;

public class ValueNotFoundException : Exception
{

    public ValueNotFoundException(string collectionName, string valueName) : base(
        $"{Resources.Exceptions_ValueNotFound.Replace("{x}", $"'{valueName}'")}: {collectionName}")
    {
            
    }
}