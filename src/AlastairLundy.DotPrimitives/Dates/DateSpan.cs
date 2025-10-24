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
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace AlastairLundy.DotPrimitives.Dates;

/// <summary>
/// Represents a span of time in terms of days, months, and years.
/// This struct encapsulates methods for comparing, parsing, and performing operations on time spans.
/// </summary>
public struct DateSpan : IEquatable<DateSpan>, IComparable<DateSpan>, 
    IComparable, ISpanParsable<DateSpan>
{
    /// <summary>
    /// Gets the number of individual days in the time span component.
    /// This property represents only the days portion, excluding any months or years.
    /// </summary>
    public double Days { get; private set; }

    /// <summary>
    /// Gets the number of individual months in the time span component.
    /// This property represents only the months portion, excluding any days or years.
    /// </summary>
    public int Months { get; private set; }

    /// <summary>
    /// Gets the number of individual years in the time span component.
    /// This property represents only the years portion, excluding any months or days.
    /// </summary>
    public int Years { get; private set; }

    /// <summary>
    /// Gets the total number of days in the time span, including contributions from months and years.
    /// This property provides the complete duration in days, combining all components of the time span.
    /// </summary>
    public double TotalDays { get; private set; }

    /// <summary>
    /// Gets the total number of months represented in the time span, including contributions from years and days.
    /// This property provides a cumulative count of months, where each year contributes 12 months and any additional days are factored into the total.
    /// </summary>
    public int TotalMonths { get; private set; }

    /// <summary>
    /// Gets the total number of years represented by the time span.
    /// This includes the cumulative years accounting for all components of the time span,
    /// including days and months, converted to their equivalent in years.
    /// </summary>
    public int TotalYears { get; private set; }


    /// <inheritdoc />
    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj is null)
            return false;
        
        if (obj is DateSpan span)
        {
            return Equals(span);
        }

        return false;
    }


    /// <inheritdoc />
    public override int GetHashCode()
    {
        return HashCode.Combine(Days, Months, Years, TotalDays, TotalMonths, TotalYears);
    }

    /// <summary>
    /// Determines whether two instances of <see cref="DateSpan"/> are equal.
    /// </summary>
    /// <param name="left">The first <see cref="DateSpan"/> instance to compare.</param>
    /// <param name="right">The second <see cref="DateSpan"/> instance to compare.</param>
    /// <returns><c>true</c> if the two <see cref="DateSpan"/> instances are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(DateSpan left, DateSpan right)
    {
        return left.Equals(right);
    }

    /// <summary>
    /// Compares two <see cref="DateSpan"/> instances for inequality.
    /// </summary>
    /// <param name="left">The first <see cref="DateSpan"/> to compare.</param>
    /// <param name="right">The second <see cref="DateSpan"/> to compare.</param>
    /// <returns>
    /// <c>true</c> if the two instances are not equal; otherwise, <c>false</c>.
    /// </returns>
    public static bool operator !=(DateSpan left, DateSpan right)
    {
        return left.Equals(right) == false;
    }

    /// <summary>
    /// Determines whether the first DateSpan instance is less than the second DateSpan instance
    /// based on their total days.
    /// </summary>
    /// <param name="left">The first DateSpan instance to compare.</param>
    /// <param name="right">The second DateSpan instance to compare.</param>
    /// <returns>True if the total days of the first instance are less than the second; otherwise, false.</returns>
    public static bool operator <(DateSpan left, DateSpan right)
        => left.TotalDays < right.TotalDays;

    /// <summary>
    /// Determines whether the left <see cref="DateSpan"/> is greater than the right <see cref="DateSpan"/>.
    /// </summary>
    /// <param name="left">The left-hand <see cref="DateSpan"/> operand.</param>
    /// <param name="right">The right-hand <see cref="DateSpan"/> operand.</param>
    /// <returns>
    /// <c>true</c> if the total days of the left operand are greater than those of the right operand; otherwise, <c>false</c>.
    /// </returns>
    public static bool operator >(DateSpan left, DateSpan right)
        => left.TotalDays > right.TotalDays;

    /// <summary>
    /// Determines whether the first DateSpan instance is less than or equal to
    /// the second DateSpan instance based on their total days.
    /// </summary>
    /// <param name="left">The first DateSpan instance to compare.</param>
    /// <param name="right">The second DateSpan instance to compare.</param>
    /// <returns>True if the first instance is less than or equal to the second; otherwise, false.</returns>
    public static bool operator <=(DateSpan left, DateSpan right)
        => left < right || left.Equals(right);

    /// <summary>
    /// Indicates whether the left <see cref="DateSpan"/> operand is greater than or equal to the right <see cref="DateSpan"/> operand.
    /// </summary>
    /// <param name="left">The left-hand <see cref="DateSpan"/> operand.</param>
    /// <param name="right">The right-hand <see cref="DateSpan"/> operand.</param>
    /// <returns>
    /// <c>true</c> if the total days of the left operand are greater than or equal to those of the right operand; otherwise, <c>false</c>.
    /// </returns>
    public static bool operator >=(DateSpan left, DateSpan right)
        => left > right || left.Equals(right);

    /// <summary>
    /// Multiplies the provided <see cref="DateSpan"/> by the specified factor.
    /// </summary>
    /// <param name="factor">The multiplication factor.</param>
    /// <param name="dateSpan">The <see cref="DateSpan"/> to be multiplied.</param>
    /// <returns>A new <see cref="DateSpan"/> with its components scaled by the specified factor.</returns>
    public static DateSpan operator *(double factor, DateSpan dateSpan)
        => new(dateSpan.TotalDays * factor, 0, 0);

    /// <summary>
    /// Implements the multiplication operator for scaling a <see cref="DateSpan"/> by a specified factor.
    /// </summary>
    /// <param name="date">The <see cref="DateSpan"/> instance to scale.</param>
    /// <param name="factor">The multiplier by which the <see cref="DateSpan"/> is scaled.</param>
    /// <returns>A new <see cref="DateSpan"/> that is the result of scaling the original span by the specified factor.</returns>
    public static DateSpan operator *(DateSpan date, double factor)
        => factor * date;

    
    public override string ToString()
    {
        
    }

    public string ToString(IFormatProvider formatProvider)
    {
        
    }
    
    /// <inheritdoc />
    public int CompareTo(object? obj)
    {
        if(obj is null)
            return 1;
        
        if (obj is DateSpan span)
            return CompareTo(span);

        throw new ArgumentException();
    }

    /// <inheritdoc />
    public bool Equals(DateSpan other)
    {
        return TotalYears == other.TotalYears &&
               TotalMonths == other.TotalMonths &&
               TotalDays.Equals(other.TotalDays) &&
               Years == other.Years &&
               Months == other.Months &&
               Days.Equals(other.Days);
    }

    /// <inheritdoc />
    public int CompareTo(DateSpan other)
    {
        if (this < other)
            return -1;
        if (this > other)
            return 1;
        
        return 0;
    }

    private int CountTotalDays(double days, int months, int years)
    {
        double total = 0;
        total += days;
        
        
    }
    
    private int CountTotalMonths(double days, int months, int years)
    {
        int total = 0;
        
        // TODO: Add days.
        total += months;
        total += (years / 12);

        return total;
    }
    
    private int CountTotalYears(double days, int months, int years)
    {
        int total = 0;
        double monthsIntoYears = Convert.ToDouble(months) / 12;
        
        // TODO: Add Days.
        total += Convert.ToInt32(monthsIntoYears);
        total += years;

        return total;
    }

    /// <summary>
    /// Represents a span of time in terms of days, months, and years.
    /// Provides properties and methods for comparing, parsing, and performing operations on time spans.
    /// </summary>
    public DateSpan(long ticks)
    {
        TimeSpan span = new TimeSpan(ticks);

        double days = span.TotalDays;
        
        TotalDays = span.TotalDays;
        TotalMonths = CountTotalMonths(days, 0, 0);
        TotalYears = CountTotalYears(days, 0, 0);
    }

    /// <summary>
    /// Represents a span of time in terms of days, months, and years.
    /// </summary>
    public DateSpan(double days, int months, int years)
    {
        Days = days;
        Months = months;
        Years = years;

        TotalDays = CountTotalDays(days, months, years);
        TotalMonths = CountTotalMonths(days, months, years);
        TotalYears = CountTotalYears(days, months, years);
    }


    /// <summary>
    /// Converts the specified string representation of a date span to its <see cref="DateSpan"/> equivalent.
    /// </summary>
    /// <param name="s">A string containing the date span representation to convert.</param>
    /// <returns>A <see cref="DateSpan"/> equivalent to the date span represented by <paramref name="s"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="s"/> is <c>null</c> or an empty string.</exception>
    /// <exception cref="FormatException">Thrown when <paramref name="s"/> is not in a valid format.</exception>
    public static DateSpan Parse(string s)
        => Parse(s, null);

    
    public static DateSpan Parse(string s, IFormatProvider? provider)
    {
        if(string.IsNullOrEmpty(s))
            throw new ArgumentNullException(nameof(s));

        provider ??= DateTimeFormatInfo.CurrentInfo;

        
    }

    /// <summary>
    /// Tries to parse the specified string representation of a date span in a specific format and culture-specific format provider.
    /// </summary>
    /// <param name="s">The string containing the value to parse.</param>
    /// <param name="provider">An object that supplies culture-specific formatting information.</param>
    /// <param name="result">When this method returns, contains the parsed <see cref="DateSpan"/> value, if parsing succeeds; otherwise, the default value of <see cref="DateSpan"/>.</param>
    /// <returns><c>true</c> if the parsing was successful; otherwise, <c>false</c>.</returns>
    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, out DateSpan result)
    {
        if (string.IsNullOrEmpty(s))
        {
            result = default;
            return false;
        }
        
        try
        {
            result = Parse(s, provider);
            return true;
        }
        catch
        {
            result = default;
            return false;
        }
    }

    public static DateSpan Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
    {
        if(s.IsEmpty)
            throw new OverflowException(nameof(s));
        
        
    }

    /// <summary>
    /// Attempts to parse a span of characters into a <see cref="DateSpan"/> structure.
    /// Returns a value indicating whether the parsing succeeded.
    /// </summary>
    /// <param name="s">The span of characters to parse.</param>
    /// <param name="provider">An object that provides culture-specific formatting information.</param>
    /// <param name="result">When this method returns, contains the resulting <see cref="DateSpan"/> structure if parsing succeeded, or the default value of <see cref="DateSpan"/> if parsing failed.</param>
    /// <returns><see langword="true"/> if the parsing succeeded; otherwise, <see langword="false"/>.</returns>
    public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, out DateSpan result)
    {
        try
        {
            result = Parse(s, provider);
            return true;
        }
        catch
        {
            result = default;
            return false;
        }
    }

    /// <summary>
    /// Creates a <see cref="DateSpan"/> instance representing the specified number of days.
    /// </summary>
    /// <param name="days">The total number of days to represent in the resulting <see cref="DateSpan"/>.</param>
    /// <returns>A <see cref="DateSpan"/> instance with the specified number of days.</returns>
    public static DateSpan FromDays(double days)
        => new(days, 0, 0);

    /// <summary>
    /// Creates a <see cref="DateSpan"/> from the specified number of months.
    /// </summary>
    /// <param name="months">The total number of months used to create the time span.</param>
    /// <returns>A <see cref="DateSpan"/> representing the given number of months.</returns>
    public static DateSpan FromMonths(int months)
        => new(0, months, 0);

    /// <summary>
    /// Creates a <see cref="DateSpan"/> instance representing the specified number of years.
    /// </summary>
    /// <param name="years">The number of years to represent in the <see cref="DateSpan"/>.</param>
    /// <returns>A <see cref="DateSpan"/> instance with the specified number of years.</returns>
    public static DateSpan FromYears(int years)
        => new(0, 0, years);
}