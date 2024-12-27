/*
    Resyslib
    Copyright (c) 2024 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;

namespace Resyslib
{
 #if NET8_0_OR_GREATER
    public class ArgumentModel(Type argumentType, object argument)
    {
#else
    public class ArgumentModel
    {
        public ArgumentModel(Type argumentType, object argument)
        {
            ArgumentProvided = argument;
            ArgumentType = argumentType;
        }
#endif
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