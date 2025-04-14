/*
    Resyslib
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */


using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

using AlastairLundy.Resyslib.Annotations.Arguments.Builders;
using AlastairLundy.Resyslib.Annotations.Exceptions;

using AlastairLundy.Resyslib.Localizations;

// ReSharper disable EntityNameCapturedOnly.Local

namespace AlastairLundy.Resyslib.Annotations.Arguments.Attributes
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(validOn: AttributeTargets.Parameter
        & AttributeTargets.GenericParameter, AllowMultiple = true)]
    public class ParameterValueConflictAttribute : ValidationAttribute
    {
        private readonly string _conflictingParameterName;
        
        private readonly object[] _conflictingValues;
        
        private readonly ArgumentConflictType _conflictType;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="potentiallyConflictingParameter"></param>
        /// <param name="conflictingValue"></param>
        public ParameterValueConflictAttribute(object potentiallyConflictingParameter, object conflictingValue)
        {
            _conflictingParameterName = nameof(potentiallyConflictingParameter);
            // ReSharper disable once UseCollectionExpression
            _conflictingValues = new[]{conflictingValue};
            _conflictType = ArgumentConflictType.Other;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="potentiallyConflictingParameter"></param>
        /// <param name="conflictingValues"></param>
        public ParameterValueConflictAttribute(object potentiallyConflictingParameter, object[] conflictingValues)
        {
            _conflictingParameterName = nameof(potentiallyConflictingParameter);
            _conflictingValues = conflictingValues;
            conflictingValues.CopyTo(_conflictingValues, 0);
            _conflictType = ArgumentConflictType.Other;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="potentiallyConflictingParameter"></param>
        /// <param name="conflictingValues"></param>
        /// <param name="conflictingType"></param>
        public ParameterValueConflictAttribute(object potentiallyConflictingParameter, object[] conflictingValues, ArgumentConflictType conflictingType)
        {
            _conflictingParameterName = nameof(potentiallyConflictingParameter);
            _conflictingValues = conflictingValues;
            conflictingValues.CopyTo(_conflictingValues, 0);
            _conflictType = conflictingType;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            ParameterInfo[]? parameters = validationContext.ObjectInstance.GetType().GetMethod(validationContext.DisplayName)?.GetParameters();
            object[]? arguments = validationContext.ObjectInstance.GetType().GetMethod(validationContext.DisplayName)?.Invoke(validationContext.ObjectInstance, null) as object[];

            if (parameters != null)
            {
                int conflictingParameterIndex = Array.IndexOf(parameters, parameters.FirstOrDefault(p => p.Name == _conflictingParameterName));
            
                if (conflictingParameterIndex >= 0 && _conflictingValues.Any(x => x.Equals(arguments?[conflictingParameterIndex])))
                {
                    return new ValidationResult(Resources.Exceptions_ArgumentConflict_CurrentParameter.Replace("{arg1}", _conflictingParameterName));
                }
                
                return ValidationResult.Success;
            }

            throw new NullReferenceException(Resources.Exceptions_ArgumentConflictBuilder_NullArgument.Replace("{arg1}", validationContext.MemberName));
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentConflictException"></exception>
        public override bool IsValid(object? value)
        {
            bool isValid = true;

            if (value != null && _conflictingValues.Any(x => x.Equals(value)))
            {
                // ReSharper disable once RedundantAssignment
                isValid = false;
                        
                ConflictingArgumentsModel conflictResult = ConflictingArgumentsBuilder.Create()
                    .WithArgument(value)
                    .WithConflictType(_conflictType)
                    .Build();

                throw new ArgumentConflictException(conflictResult);
            }
            
            return isValid;
        }
    }
}