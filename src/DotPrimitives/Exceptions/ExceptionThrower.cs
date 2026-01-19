/*
    MIT License
   
    Copyright (c) 2025-2026-2026 Alastair Lundy
   
    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:
   
    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.
   
    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.
 */

#pragma warning disable CS1723 // XML comment has cref attribute that refers to a type parameter
namespace DotPrimitives.Exceptions;

/// <summary>
/// Provides utility methods for conditionally throwing exceptions.
/// </summary>
public static class ExceptionThrower
{
    /// <summary>
    /// Throws the specified exception if the given expression evaluates to true.
    /// </summary>
    /// <param name="expression">A boolean expression that determines whether the exception will be thrown.</param>
    /// <param name="exception">The exception to throw if the expression evaluates to true.</param>
    /// <exception cref="Exception">Thrown when the expression evaluates to true.</exception>
    public static void ThrowIf(bool expression, Exception exception)
    {
        if (expression)
            throw exception;
    }

    /// <summary>
    /// Throws the specified exception if the given predicate evaluates to true for the provided item.
    /// </summary>
    /// <param name="predicate">A function that evaluates a condition on the given item.</param>
    /// <param name="item">The item to be evaluated by the predicate.</param>
    /// <param name="exception">The exception to throw if the predicate evaluates to true for the item.</param>
    /// <typeparam name="T">The type of the item being evaluated.</typeparam>
    /// <exception cref="Exception">Thrown when the predicate evaluates to true for the given item.</exception>
    public static void ThrowIf<T>(Func<T, bool> predicate, T item, Exception exception)
    {
        if (predicate(item))
            throw exception;
    }
    
    /// <typeparam name="TException"></typeparam>
    extension<TException>(TException)
        where TException : Exception, new()
    {
        /// <summary>
        /// Throws the specified exception type if the given expression evaluates to true.
        /// </summary>
        /// <typeparam name="T">The type of the object to evaluate in the predicate.</typeparam>
        /// <typeparam name="TException">The type of the exception to throw if the expression evaluates to true. Must derive from <see cref="Exception"/> and have a parameterless constructor.</typeparam>
        /// <param name="predicate">A function that takes an instance of type <typeparamref name="T"/> and returns a boolean indicating whether the exception should be thrown.</param>
        /// <param name="item">The object of type <typeparamref name="T"/> to evaluate with the provided predicate.</param>
        /// <exception cref="TException">Thrown when the predicate evaluates to true for the given item.</exception>
        public static void ThrowIf<T>(Func<T, bool> predicate, T item)
        {
            if (predicate(item))
                throw new TException();
        }

        /// <summary>
        /// Throws the specified exception of type <typeparamref name="TException"/>
        /// if the provided expression evaluates to true.
        /// </summary>
        /// <typeparam name="TException">The type of the exception to throw. Must be a subclass of <see cref="Exception"/> and have a parameterless constructor.</typeparam>
        /// <param name="expression">A boolean value representing whether to throw the exception.</param>
        /// <exception cref="TException">Thrown when the expression evaluates to true.</exception>
        public static void ThrowIf(bool expression)
        {
            if (expression)
                throw new TException();
        }
    }
}