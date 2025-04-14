/*
    Resyslib.Collections
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;

using AlastairLundy.Resyslib.Collections.Internal.Localizations;

// ReSharper disable ConvertToPrimaryConstructor

namespace AlastairLundy.Resyslib.Collections.Exceptions
{
    public class KeyValuePairNotFoundException : Exception
    {

        public KeyValuePairNotFoundException(string collectionName) : base(
            $"{Resources.Exceptions_KeyValuePairNotFound}: {collectionName}")
        {
            
        }
    }
}