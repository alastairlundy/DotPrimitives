/*
    Comments and Property Names by .NET Foundation from .NET API Reference Docs - https://github.com/dotnet/dotnet-api-docs  - are licensed under CC-BY-4.0. 
    Please see THIRD_PARTY_NOTICES.txt file for more details.
    
    Some comments are not from the .NET API Reference Docs.
        
    All other code is licensed under the MIT License - Please see the LICENSE.txt file for more details.
 */

using System;

namespace Resyslib.Drawing.Printing.Models
{
    /// <summary>
    /// Specifies the dimensions of the margins of a printed page.
    /// </summary>
    public class Margins : ICloneable, IEquatable<Margins>
    {
        private int _bottom;
        private int _left;
        private int _right;
        private int _top;
        
        /// <summary>
        /// Initializes a new instance of the Margins class with 1-inch wide margins.
        /// </summary>
        public Margins()
        {
            Bottom = 100;
            Left = 100;
            Right = 100;
            Top = 100;
        }

        /// <summary>
        /// Initializes a new instance of the Margins class with the specified left, right, top, and bottom margins.
        /// </summary>
        /// <param name="bottom">The bottom margin, in hundredths of an inch.</param>
        /// <param name="left">The left margin, in hundredths of an inch.</param>
        /// <param name="right">The right margin, in hundredths of an inch.</param>
        /// <param name="top">The top margin, in hundredths of an inch.</param>
        public Margins(int bottom, int left, int right, int top)
        {
            Bottom = bottom;
            Left = left;
            Right = right;
            Top = top;
        }

        
        /// <summary>
        /// Gets or sets the bottom margin, in hundredths of an inch.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown if the value provided is a negative value.</exception>
        public int Bottom
        {
            get => _bottom;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException($"Cannot set Bottom margin to negative value: {value}");
                }
                else
                {
                    _bottom = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the left margin width, in hundredths of an inch.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown if the value provided is a negative value.</exception>
        public int Left
        {
            get => _left;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException($"Cannot set Left margin to negative value: {value}");
                }
                else
                {
                    _left = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the right margin width, in hundredths of an inch.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown if the value provided is a negative value.</exception>
        public int Right
        {
            get => _right;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException($"Cannot set Right margin to negative value: {value}");
                }
                else
                {
                    _right = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the top margin width, in hundredths of an inch.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown if the value provided is a negative value.</exception>
        public int Top
        {
            get => _top;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException($"Cannot set Top margin to negative value: {value}");
                }
                else
                {
                    _top = value;
                }
            }
        }
        
        /// <summary>
        /// Retrieves a duplicate of this object, member by member.
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
           return new Margins(Bottom, Left, Right, Top);
        }

        /// <summary>
        /// Calculates and retrieves a hash code based on the width of the left, right, top, and bottom margins.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return $"L{Left}B{Bottom}R{Right}T{Top}".GetHashCode();
        }
        
        /// <summary>
        /// Compares this Margins to the specified Margins object to determine whether they have the same dimensions.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Margins other)
        {
            if (other == null)
            {
                return false;
            }
            
            return other.Bottom == Bottom &&
                   other.Left == Left &&
                   other.Right == Right &&
                   other.Top == Top;
        }

        /// <summary>
        /// Converts the Margins to a string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Left}/100ths of an Inch x {Top}/100ths of an Inch x {Right}/100ths of an Inch x {Bottom}/100ths of an Inch";
        }

#if NET_8_0_OR_GREATER || NETSTANDARD2_1
        /// <summary>
        /// Compares this Margins to the specified Object to determine whether they have the same dimensions.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                return false;
            }
            
            if (obj is Margins margins)
            {
                return Equals(margins);
            }
            else
            {
                return false;
            }
        }
#elif NETSTANDARD2_0
        /// <summary>
        /// Compares this Margins to the specified Object to determine whether they have the same dimensions.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is Margins margins)
            {
                return Equals(margins);
            }
            else
            {
                return false;
            }
        }     
#endif
    }
}