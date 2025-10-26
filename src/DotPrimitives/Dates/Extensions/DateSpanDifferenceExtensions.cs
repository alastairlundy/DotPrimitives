/*
    MIT License
   
    Copyright (c) 2025 Alastair Lundy
   
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

using System;
// ReSharper disable CheckNamespace

namespace AlastairLundy.DotPrimitives.Dates;

/// <summary>
/// Provides extension methods for calculating the difference between two dates
/// using <see cref="DateSpan"/> to represent the result.
/// </summary>
public static class DateSpanDifferenceExtensions
{
    /// <summary>
    /// Calculates the difference between two <see cref="DateTime"/> values and returns the result as a <see cref="DateSpan"/>.
    /// </summary>
    /// <param name="first">The first <see cref="DateTime"/> value in the calculation.</param>
    /// <param name="second">The second <see cref="DateTime"/> value in the calculation.</param>
    /// <returns>A <see cref="DateSpan"/> representing the years, months, and days difference between the two <see cref="DateTime"/> values.</returns>
    public static DateSpan Difference(this DateTime first, DateTime second)
    {
        int yearDifference = Math.Abs(first.Year - second.Year);

        if (yearDifference == 0)
        {
            TimeSpan dayTimeSpan = TimeSpan.FromDays(Math.Abs(first.DayOfYear - second.DayOfYear));

            return new DateSpan(dayTimeSpan.TotalDays, 0, 0);
        }
        
        double days = 0;

        int startYear = first.Year <  second.Year ? first.Year : second.Year;
        int endYear = startYear == first.Year ? second.Year : first.Year;

        for (int year = startYear; year <= endYear; year++)
        {
            if (year == endYear)
            {
                int startMonth, endMonth;

                if (first.Month > second.Month)
                {
                    startMonth = second.Month;
                    endMonth = first.Month;
                }
                else
                {
                    startMonth = first.Month;
                    endMonth = second.Month;
                }
                
                for (int month = startMonth; month <= endMonth; month++)
                {
                    days += DateTime.DaysInMonth(year, month);
                }
            }
            else
            {
                for (int month = 1; month <= 12; month++)
                {
                    days += DateTime.DaysInMonth(year, month);
                }
            }

        }
        days += Math.Abs(first.Day - second.Day);

        return new DateSpan(days, 0, 0);
    }

#if NET8_0_OR_GREATER
    /// <summary>
    /// Calculates the difference between two <see cref="DateOnly"/> values and returns the result as a <see cref="DateSpan"/>.
    /// </summary>
    /// <param name="first">The first <see cref="DateOnly"/> value in the calculation.</param>
    /// <param name="second">The second <see cref="DateOnly"/> value in the calculation.</param>
    /// <returns>A <see cref="DateSpan"/> representing the years, months, and days difference between the two <see cref="DateOnly"/> values.</returns>
    public static DateSpan Difference(this DateOnly first, DateOnly second)
        => Difference(DateTime.Parse(first.ToLongDateString()), 
            DateTime.Parse(second.ToLongDateString()));
#endif
}