/*
    Resyslib
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */


using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using AlastairLundy.Resyslib.Internal.Localizations;

// ReSharper disable MemberCanBePrivate.Global

// ReSharper disable ConvertToPrimaryConstructor

namespace AlastairLundy.Resyslib.Annotations.Arguments.Builders;

/// <summary>
/// A fluent interface style builder for creating a ConflictingArgumentsModels. 
/// </summary>
public class ConflictingArgumentsBuilder
{
    private readonly ConflictingArgumentsModel _conflictingArgumentsModel;

    /// <summary>
    /// A fluent interface style builder for creating a ConflictingArgumentsModels. 
    /// </summary>
    public ConflictingArgumentsBuilder()
    {
        _conflictingArgumentsModel =
            new ConflictingArgumentsModel(new List<ArgumentModel>(), ArgumentConflictType.Other);
    }

    /// <summary>
    /// A fluent interface style builder for creating a ConflictingArgumentsModels. 
    /// </summary>
    /// <param name="conflictingArgumentsModel">The conflicting arguments model.</param>
    public ConflictingArgumentsBuilder(ConflictingArgumentsModel conflictingArgumentsModel)
    {
        _conflictingArgumentsModel = conflictingArgumentsModel;
    }

    /// <summary>
    /// Creates an instance of the ConflictingArgumentsBuilder class.
    /// </summary>
    /// <returns>The newly created instance of the ConflictingArgumentsBuilder class.</returns>
    [Pure]
    public static ConflictingArgumentsBuilder Create()
    {
        return new ConflictingArgumentsBuilder();
    }
        
    /// <summary>
    /// 
    /// </summary>
    /// <param name="conflictType"></param>
    /// <returns></returns>
    [Pure]
    public ConflictingArgumentsBuilder WithConflictType(ArgumentConflictType conflictType)
    {
        return new ConflictingArgumentsBuilder(
            new ConflictingArgumentsModel(_conflictingArgumentsModel.ConflictingArguments, conflictType));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="argument"></param>
    /// <returns></returns>
    [Pure]
    public ConflictingArgumentsBuilder WithArgument(ArgumentModel argument)
    {
        List<ArgumentModel> newList = _conflictingArgumentsModel.ConflictingArguments.ToList();
            
        newList.Add(new ArgumentModel(argument.GetType(), argument));
            
        return new ConflictingArgumentsBuilder(new ConflictingArgumentsModel(newList, _conflictingArgumentsModel.ConflictType));
    }
        
    /// <summary>
    /// 
    /// </summary>
    /// <param name="argument"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="NullReferenceException">Thrown if the argument provided is null.</exception>
    [Pure]
    public ConflictingArgumentsBuilder WithArgument<T>(T argument)
    {
        if (argument == null)
        {
            throw new NullReferenceException(Resources.Exceptions_ArgumentConflictBuilder_NullArgument.Replace("{arg}", nameof(argument)));
        }
            
        return WithArgument(new ArgumentModel(argument.GetType(), argument));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public ConflictingArgumentsModel Build()
    {
        return _conflictingArgumentsModel;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public (ArgumentModel[] Arguments, ArgumentConflictType ConflictType) BuildToTuple()
    {
        return (_conflictingArgumentsModel.ConflictingArguments, _conflictingArgumentsModel.ConflictType);
    }
}