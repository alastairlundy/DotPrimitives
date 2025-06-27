/*
    AlastairLundy.Primitives
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */


using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AlastairLundy.Resyslib.Internal.Localizations;

// ReSharper disable RedundantBoolCompare
// ReSharper disable MemberCanBePrivate.Global

namespace AlastairLundy.Resyslib.Annotations.Arguments;

/// <summary>
/// An attribute to specify invalid parameter argument values for a parameter.
/// </summary>
/// <remarks>This attribute will validate the parameter value against the array of invalid values.</remarks>
[AttributeUsage(validOn: AttributeTargets.Parameter & AttributeTargets.GenericParameter, AllowMultiple = true)]
public class InvalidParameterValuesAttribute : ValidationAttribute
{
    private readonly object[] _invalidValues;
        
    public object[] InvalidValues => _invalidValues;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="invalidValues"></param>
    public InvalidParameterValuesAttribute(params object[] invalidValues) : base(Resources.Errors_Attributes_InvalidValue)
    {
        _invalidValues = invalidValues;
        invalidValues.CopyTo(_invalidValues, 0);
    }
        
    /// <summary>
    /// 
    /// </summary>
    /// <param name="invalidValues"></param>
    /// <param name="errorMessage"></param>
    public InvalidParameterValuesAttribute(object[] invalidValues, string errorMessage) : base(errorMessage)
    {
        _invalidValues = invalidValues;
        invalidValues.CopyTo(_invalidValues, 0);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        bool isValid = false;

        if (value != null && value.Equals(validationContext.ObjectInstance))
        {
            isValid = InvalidValues.Any(x => x.Equals(value));
        }

        if (isValid == true)
        {
            return ValidationResult.Success;
        }

        return new ValidationResult(ErrorMessage);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public override bool IsValid(object? value)
    {
        bool isValid = true;

        if (value != null)
        {
            isValid = InvalidValues.Any(x => x.Equals(value));
        }

        return isValid;
    }
}