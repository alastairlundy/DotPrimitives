/*
    OldResyslib
    Copyright (c) 2024 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

namespace AlastairLundy.Resyslib
{
    /// <summary>
    /// An enum to represent different Argument Conflict types.
    /// </summary>
    public enum ArgumentConflictType
    { 
        /// <summary>
        /// One or more provided arguments effectively or directly overrides the value of one or more other provided argument(s).
        /// </summary>
        OverriddenArgument,
        /// <summary>
        /// The provided arguments effectively or directly require conflicting or differing action to be taken.
        /// </summary>
        ArgumentsAreMutuallyExclusive,
        ArgumentValueIsInvalid,
        ArgumentTypeIsInvalid,
        Other
    }
}