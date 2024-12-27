/*
    Resyslib
    Copyright (c) 2024 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;
// ReSharper disable ConvertToPrimaryConstructor

namespace Resyslib
{
    /// <summary>
    /// A class to represent an Argument. 
    /// </summary>
    public class ArgumentModel
    {
        public ArgumentModel(Type argumentType, object argument)
        {
            ArgumentProvided = argument;
            ArgumentType = argumentType;
        }

        /// <summary>
        /// The argument provided.
        /// </summary>
        public object ArgumentProvided { get; }

        /// <summary>
        /// The type of the argument.
        /// </summary>
        public Type ArgumentType { get; }
    }
}