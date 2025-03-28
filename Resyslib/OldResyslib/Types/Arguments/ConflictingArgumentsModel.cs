/*
    OldResyslib
    Copyright (c) 2024 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace AlastairLundy.Resyslib
{
    public class ConflictingArgumentsModel
    {
        /// <summary>
        /// The conflicting arguments.
        /// </summary>
        public ArgumentModel[] ConflictingArguments { get; protected set; }
    
        /// <summary>
        /// The type of conflict that has occurred.
        /// </summary>
        public ArgumentConflictType ConflictType { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conflictingArguments"></param>
        /// <param name="conflictType"></param>
        public ConflictingArgumentsModel(IEnumerable<ArgumentModel> conflictingArguments, ArgumentConflictType conflictType)
        {
            ConflictingArguments = conflictingArguments.ToArray();
            ConflictType = conflictType;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conflictingArguments"></param>
        /// <param name="conflictType"></param>
        public ConflictingArgumentsModel(ArgumentModel[] conflictingArguments, ArgumentConflictType conflictType)
        {
            ConflictingArguments = conflictingArguments;
            ConflictType = conflictType;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public (ArgumentModel[] Arguments, ArgumentConflictType ConflictType) ToImplicitTuple()
        {
            return (ConflictingArguments, ConflictType);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Tuple<ArgumentModel[], ArgumentConflictType> ToTuple()
        {
            return new Tuple<ArgumentModel[], ArgumentConflictType>(ConflictingArguments, ConflictType);
        }
    }
}