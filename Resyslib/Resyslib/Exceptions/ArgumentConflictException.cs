/*
    Resyslib
    Copyright (c) 2024 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections.Generic;

using Resyslib.Internal.Localizations;
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Resyslib.Exceptions
{
    /// <summary>
    /// The exception that is thrown when multiple arguments conflict with one another.
    /// </summary>
    public class ArgumentConflictException : Exception
    {
        /// <summary>
        /// The model representing the conflicting arguments and the type of conflict.
        /// </summary>
        public ConflictingArgumentsModel ConflictingArguments { get; protected set; }
        
        /// <summary>
        /// The exception that is thrown when multiple arguments conflict with one another.
        /// </summary>
        /// <param name="conflictingArguments">The conflicting arguments.</param>
        /// <param name="conflictType">The type of conflict that has occurred.</param>
        public ArgumentConflictException(IEnumerable<ArgumentModel> conflictingArguments, ArgumentConflictType conflictType) : base(Resources.Exceptions_ArgumentConflict)
        {
            ConflictingArguments = new ConflictingArgumentsModel(conflictingArguments, conflictType);
        }
        
        /// <summary>
        /// The exception that is thrown when multiple arguments conflict with one another.
        /// </summary>
        /// <param name="conflictingArguments">The conflicting arguments.</param>
        public ArgumentConflictException(ConflictingArgumentsModel conflictingArguments) : base(Resources.Exceptions_ArgumentConflict)
        {
            ConflictingArguments = conflictingArguments;
        }
    }
}