/*
    Resyslib.Collections
    Copyright (c) 2024 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;

using Resyslib.Collections.Internal.Localizations;

namespace Resyslib.Collections.Internal.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class KeyValuePairNotFoundException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="collectionName"></param>
        public KeyValuePairNotFoundException(string collectionName) : base($"{Resources.Exceptions_KeyValuePairNotFound}: {collectionName}")
        {
            
        }
    }
}