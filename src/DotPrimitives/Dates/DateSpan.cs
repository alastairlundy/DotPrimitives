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
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Text;

// ReSharper disable RedundantToStringCall

namespace AlastairLundy.DotPrimitives.Dates;

/// <summary>
/// Represents a span of time in terms of days, months, and years.
/// This struct encapsulates methods for comparing, parsing, and performing operations on date spans.
/// </summary>
public readonly struct DateSpan : IEquatable<DateSpan>, IComparable<DateSpan>, IFormattable,
    IComparable
#if NET8_0_OR_GREATER
,ISpanParsable<DateSpan>
#endif
{
    /// <summary>
    /// Gets the number of individual days in the date span component.
    /// This property represents only the day portion, excluding any months or years.
    /// </summary>
    public decimal Days { get; }

    /// <summary>
    /// Gets the number of individual months in the date span component.
    /// This property represents only the month portion, excluding any days or years.
    /// </summary>
    public int Months { get; }

    /// <summary>
    /// Gets the number of individual years in the date span component.
    /// This property represents only the year portion, excluding any months or days.
    /// </summary>
    public int Years { get; }

    /// <summary>
    /// Gets the total number of days in the date span, including contributions from months and years.
    /// This property provides the complete duration in days, combining all components of the date span.
    /// </summary>
    public decimal TotalDays { get; }

    /// <summary>
    /// Gets the total number of months represented in the date span, including contributions from years and days.
    /// This property provides a cumulative count of months, where each year contributes 12 months and any additional days are factored into the total.
    /// </summary>
    public decimal TotalMonths { get; }

    /// <summary>
    /// Gets the total number of years represented by the date span.
    /// This includes the cumulative years accounting for all components of the date span,
    /// including days and months, converted to their equivalent duration in years.
    /// </summary>
    public decimal TotalYears { get; }

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
    public static bool operator ==(DateSpan left, DateSpan right) => left.Equals(right);

    /// <summary>
    /// Compares two <see cref="DateSpan"/> instances for inequality.
    /// </summary>
    /// <param name="left">The first <see cref="DateSpan"/> to compare.</param>
    /// <param name="right">The second <see cref="DateSpan"/> to compare.</param>
    /// <returns>
    /// <c>true</c> if the two instances are not equal; otherwise, <c>false</c>.
    /// </returns>
    public static bool operator !=(DateSpan left, DateSpan right) => !left.Equals(right);

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
        => new((double)dateSpan.TotalDays * factor, 0, 0);

    /// <summary>
    /// Implements the multiplication operator for scaling a <see cref="DateSpan"/> by a specified factor.
    /// </summary>
    /// <param name="date">The <see cref="DateSpan"/> instance to scale.</param>
    /// <param name="factor">The multiplier by which the <see cref="DateSpan"/> is scaled.</param>
    /// <returns>A new <see cref="DateSpan"/> that is the result of scaling the original span by the specified factor.</returns>
    public static DateSpan operator *(DateSpan date, double factor)
        => factor * date;
    
    /// <summary>
    /// Multiplies the provided <see cref="DateSpan"/> by the specified factor.
    /// </summary>
    /// <param name="factor">The multiplication factor.</param>
    /// <param name="dateSpan">The <see cref="DateSpan"/> to be multiplied.</param>
    /// <returns>A new <see cref="DateSpan"/> with its components scaled by the specified factor.</returns>
    public static DateSpan operator *(decimal factor, DateSpan dateSpan)
        => new(dateSpan.TotalDays * factor, 0, 0);

    /// <summary>
    /// Implements the multiplication operator for scaling a <see cref="DateSpan"/> by a specified factor.
    /// </summary>
    /// <param name="date">The <see cref="DateSpan"/> instance to scale.</param>
    /// <param name="factor">The multiplier by which the <see cref="DateSpan"/> is scaled.</param>
    /// <returns>A new <see cref="DateSpan"/> that is the result of scaling the original span by the specified factor.</returns>
    public static DateSpan operator *(DateSpan date, decimal factor)
        => factor * date;

    /// <summary>
    /// Adds two instances of <see cref="DateSpan"/> together.
    /// </summary>
    /// <param name="leftDateSpan">The first <see cref="DateSpan"/> instance.</param>
    /// <param name="rightDateSpan">The second <see cref="DateSpan"/> instance.</param>
    /// <returns>A new <see cref="DateSpan"/> representing the sum of the input instances.</returns>
    public static DateSpan operator +(DateSpan leftDateSpan, DateSpan rightDateSpan)
        => leftDateSpan.Add(rightDateSpan);

    /// <summary>
    /// Defines the subtraction operator for <see cref="DateSpan"/> instances,
    /// allowing one <see cref="DateSpan"/> to be subtracted from another.
    /// </summary>
    /// <param name="leftDateSpan">The first <see cref="DateSpan"/> instance.</param>
    /// <param name="rightDateSpan">The second <see cref="DateSpan"/> instance.</param>
    /// <returns>A new <see cref="DateSpan"/> representing the result of the subtraction.</returns>
    public static DateSpan operator -(DateSpan leftDateSpan, DateSpan rightDateSpan)
        => leftDateSpan.Subtract(rightDateSpan);

    /// <summary>
    /// Returns the string representation of the current <see cref="DateSpan"/> instance.
    /// </summary>
    /// <returns>A string that represents the current <see cref="DateSpan"/> instance.</returns>
    public override string ToString()
        => ToString(null, null);


    /// <summary>
    /// Returns a string representation of the current <see cref="DateSpan"/> instance.
    /// </summary>
    /// <param name="format">A standard or custom numeric format string that determines the format of the returned string.</param>
    /// <param name="formatProvider">An object that supplies culture-specific formatting information. If null, the current culture is used.</param>
    /// <returns>A string that represents the current <see cref="DateSpan"/> instance formatted according to the specified format and format provider.</returns>
    /// <exception cref="NotSupportedException">Thrown when the specified <paramref name="format"/> is not supported.</exception>
    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        formatProvider ??= CultureInfo.CurrentCulture;
        format ??= "G";

#if NET8_0_OR_GREATER
        bool isNegative = decimal.IsNegative(TotalDays) ||
                          decimal.IsNegative(TotalMonths) ||
                          decimal.IsNegative(TotalYears);
        #else
        bool isNegative = TotalDays < (decimal)0.0 || TotalMonths < (decimal)0.0 || TotalYears < (decimal)0.0;
        #endif

        string result = "";

        int yearDigitCount = TotalDays.ToString(CultureInfo.InvariantCulture).Replace(".", "").Length;
        int monthsDigitCount = TotalDays.ToString(CultureInfo.InvariantCulture).Replace(".", "").Length;

        int daysDigitCount = TotalDays.ToString(CultureInfo.InvariantCulture).Replace(".", "").Length;
            
        int decimalPlaces = 2;

        if (daysDigitCount > 2 || monthsDigitCount > 2 ||  yearDigitCount > 2)
            decimalPlaces = 3;
        
        switch (format)
        {
            case "G":
                result = $"{Math.Round(TotalYears, decimalPlaces).ToString(formatProvider)}" +
                         $":{Math.Round(TotalMonths, decimalPlaces).ToString(formatProvider)}" +
                         $":{Math.Round(TotalDays, decimalPlaces).ToString(formatProvider)}";
                break;
            case "g":
                decimalPlaces = 2;
                
                result += $"{Math.Round(TotalYears, decimalPlaces).ToString(formatProvider).Replace(",0", string.Empty).Replace(".0", string.Empty)}:";

                if (TotalMonths > (decimal)0.0)
                    result += $"{Math.Round(TotalMonths, decimalPlaces).ToString(formatProvider).Replace(",0", string.Empty).Replace(".0", string.Empty)}:";
                if (TotalDays > (decimal)0.0)
                    result += $"{Math.Round(TotalDays, decimalPlaces).ToString(formatProvider).Replace(",0", string.Empty).Replace(".0", string.Empty)}:";
                break;
            case "c" or "C":
                result = $"{Math.Round(TotalYears, decimalPlaces)}:{Math.Round(TotalMonths, decimalPlaces)}:{Math.Round(TotalDays, decimalPlaces)}";
                break;
            default:
                throw new NotSupportedException();
        }

        if (isNegative)
           result = result.Insert(0, "-");

        return result;
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
        return TotalYears.Equals(other.TotalYears) &&
               TotalMonths.Equals(other.TotalMonths) &&
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

    #region Private Count Total Methods
    private decimal CountTotalDays(decimal days, decimal months, decimal years)
    {
        decimal total = 0;
        total += days;
        
        TimeSpan monthsSpan = DateTime.Now.AddMonths(Convert.ToInt32(months)).Subtract(DateTime.Now);
        TimeSpan yearsSpan = DateTime.Now.AddYears(Convert.ToInt32(years)).Subtract(DateTime.Now);

        total += Convert.ToDecimal(monthsSpan.Ticks) / Convert.ToDecimal(TimeSpan.TicksPerDay);
        total += Convert.ToDecimal(yearsSpan.Ticks) / Convert.ToDecimal(TimeSpan.TicksPerDay);
       
        return total;
    }
    
    private decimal CountTotalMonths(decimal days, decimal months, decimal years)
    {
        decimal total = 0;
               
        if (DateTime.IsLeapYear(DateTime.Now.Year))
        {
            total += ((decimal)365.2524 / 12) / days;
        }
        else
        {
            total += ((decimal)366.0 / (decimal)12.0) / days;
        }
        
        total += months;
        total += Convert.ToDecimal(years) / (decimal)12.0;

        return total;
    }
    
    private decimal CountTotalYears(decimal days, decimal months, decimal years)
    {
        decimal total = 0;
        decimal monthsIntoYears = months / 12;
        
        if (DateTime.IsLeapYear(DateTime.Now.Year))
        {
            total += ((decimal)365.2524 / 12) / days;
        }
        else
        {
            total += ((decimal)366.0 / (decimal)12.0) / days;
        }
        
        total += monthsIntoYears;
        total += years;

        return total;
    }
    #endregion


    /// <summary>
    /// Represents a span of time defined in terms of days, months, and years.
    /// </summary>
    /// <paramref name="ticks"></paramref>
    public DateSpan(long ticks)
    {
        TimeSpan span = new TimeSpan(ticks);
        
        TotalDays = (decimal)span.TotalDays;
        TotalMonths = CountTotalMonths((decimal)span.TotalDays, 0, 0);
        TotalYears = CountTotalYears((decimal)span.TotalDays, 0, 0);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="days"></param>
    /// <param name="months"></param>
    /// <param name="years"></param>
    public DateSpan(double days, double months, double years)
    {
        Days = (decimal)days;
        Months = Convert.ToInt32(months);
        Years = Convert.ToInt32(years);

        TotalDays = CountTotalDays((decimal)days, (decimal)months, (decimal)years);
        TotalMonths = CountTotalMonths((decimal)days, (decimal)months, (decimal)years);
        TotalYears = CountTotalYears((decimal)days, (decimal)months, (decimal)years);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="days"></param>
    /// <param name="months"></param>
    /// <param name="years"></param>
    public DateSpan(decimal days, decimal months, decimal years)
    {
        Days = days;
        Months = Convert.ToInt32(months);
        Years = Convert.ToInt32(years);

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


    /// <summary>
    /// Parses the string representation of a DateSpan and converts it to its DateSpan equivalent using the specified format provider.
    /// </summary>
    /// <param name="s">The string representation of a DateSpan.</param>
    /// <param name="provider">The format provider to use when interpreting the input string. If null, the current culture's DateTimeFormatInfo is used.</param>
    /// <returns>A DateSpan object equivalent to the string representation provided.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the input string <paramref name="s"/> is null or empty.</exception>
    /// <exception cref="OverflowException">Thrown when the input string <paramref name="s"/> contains invalid characters or exceeds numeric limits.</exception>
    /// <exception cref="FormatException">Thrown when the input string <paramref name="s"/> does not conform to an expected format.</exception>
    public static DateSpan Parse(string s, IFormatProvider? provider)
    {
#if NET8_0_OR_GREATER
        ArgumentException.ThrowIfNullOrEmpty(s);
#else
        if(string.IsNullOrEmpty(s))
            throw new ArgumentNullException(nameof(s));
#endif
        provider ??= DateTimeFormatInfo.CurrentInfo;

        if (s.Any(c => !char.IsDigit(c) && c != ',' && c != '.'
                       && c != ':'))
            throw new OverflowException();
            
        string format = (string?)provider.GetFormat(typeof(DateSpan)) ?? "G";
        
        string[] split = string.Format(format, s).Split(":");
        
        if (split.Length == 3)
        {
            return new DateSpan(double.Parse(split[2]), double.Parse(split[1]),
                double.Parse(split[0]));
        }
        else if (split.Length == 2)
        {
            return new DateSpan(0.0, double.Parse(split[1]),
                double.Parse(split[0]));
        }
        else if (split.Length == 1)
        {
            return new DateSpan(0.0, 0.0,
                double.Parse(split[0]));
        }
        else
        {
            throw new FormatException();
        }
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
        if (string.IsNullOrEmpty(s) || s is null)
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

    /// <summary>
    /// Converts the specified span of characters representing a date range into its <see cref="DateSpan"/> equivalent.
    /// </summary>
    /// <param name="s">A read-only span of characters containing the date range to parse.</param>
    /// <param name="provider">An object that provides culture-specific formatting information.</param>
    /// <returns>A <see cref="DateSpan"/> that is equivalent to the date range specified in <paramref name="s"/>.</returns>
    /// <exception cref="OverflowException">Thrown when <paramref name="s"/> is empty.</exception>
    /// <exception cref="FormatException">Thrown when the format of <paramref name="s"/> is invalid.</exception>
    public static DateSpan Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
    {
        if (s.IsEmpty)
            throw new OverflowException(nameof(s));

        provider ??= DateTimeFormatInfo.CurrentInfo;

        StringBuilder stringBuilder = new StringBuilder();
        
        foreach (char c in s)
        {
            if (char.IsDigit(c) == false && c == ',' == false && c == '.' == false
                && c == ':' == false)
            {
                throw new OverflowException();
            }
            
            stringBuilder.Append(c);
        }
        
        string format = (string?)provider.GetFormat(typeof(DateSpan)) ?? "G";
        string newVal = string.Format(format, stringBuilder.ToString());

        string[] split = newVal.Split(":");

        if (split.Length == 3)
        {
            return new DateSpan(double.Parse(split[2]), double.Parse(split[1]),
                double.Parse(split[0]));
        }

        if (split.Length == 2)
        {
            return new DateSpan(0.0, double.Parse(split[1]),
                double.Parse(split[0]));
        }
        if (split.Length == 1)
        {
            return new DateSpan(0.0, 0.0,
                double.Parse(split[0]));
        }
        throw new FormatException();
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
    /// 
    /// </summary>
    /// <param name="days"></param>
    /// <returns></returns>
    public static DateSpan FromDays(decimal days)
        => new(days, 0, 0);
    
    /// <summary>
    /// Creates a <see cref="DateSpan"/> from the specified number of months.
    /// </summary>
    /// <param name="months">The total number of months used to create the date span.</param>
    /// <returns>A <see cref="DateSpan"/> representing the given number of months.</returns>
    public static DateSpan FromMonths(int months)
        => new(0, Convert.ToDouble(months), 0);

    /// <summary>
    /// Creates a <see cref="DateSpan"/> instance representing the specified number of years.
    /// </summary>
    /// <param name="years">The number of years to represent in the <see cref="DateSpan"/>.</param>
    /// <returns>A <see cref="DateSpan"/> instance with the specified number of years.</returns>
    public static DateSpan FromYears(int years)
        => new(0, 0, Convert.ToDouble(years));

    /// <summary>
    /// Creates a new instance of <see cref="DateSpan"/> from the specified <see cref="TimeSpan"/>.
    /// </summary>
    /// <param name="timeSpan">The <see cref="TimeSpan"/> to convert to a <see cref="DateSpan"/>.</param>
    /// <returns>A new <see cref="DateSpan"/> instance representing the equivalent time span.</returns>
    public static DateSpan FromTimeSpan(TimeSpan timeSpan)
        => new(timeSpan.Ticks);

    /// <summary>
    /// Converts the current <see cref="DateSpan"/> instance to its equivalent <see cref="TimeSpan"/> representation.
    /// </summary>
    /// <returns>A <see cref="TimeSpan"/> instance that represents the total duration of the <see cref="DateSpan"/> in days.</returns>
    public TimeSpan ToTimeSpan() => TimeSpan.FromDays((double)TotalDays);

    /// <summary>
    /// Gets a <see cref="DateSpan"/> instance representing a zero date span,
    /// with all components (days, months, and years) set to zero.
    /// </summary>
    public static DateSpan Zero => new((double)0, 0, 0);

    /// <summary>
    /// Adds the provided <see cref="DateSpan"/> to the current instance and returns the resulting <see cref="DateSpan"/>.
    /// </summary>
    /// <param name="other">The <see cref="DateSpan"/> to add to the current instance.</param>
    /// <returns>A <see cref="DateSpan"/> representing the result of the addition.</returns>
    [Pure]
    public DateSpan Add(DateSpan other) =>
        new(TotalDays + other.TotalDays,
            TotalMonths + other.TotalMonths,
            TotalYears + other.TotalYears);

    /// <summary>
    /// Subtracts the specified <see cref="DateSpan"/> from the current instance and returns the resulting <see cref="DateSpan"/>.
    /// </summary>
    /// <param name="other">The <see cref="DateSpan"/> to subtract from the current instance.</param>
    /// <returns>A new <see cref="DateSpan"/> that represents the result of the subtraction.</returns>
    [Pure]
    public DateSpan Subtract(DateSpan other)
        => new(TotalDays - other.TotalDays,
        TotalMonths - other.TotalMonths,
        TotalYears - other.TotalYears);
    
    /// <summary>
    /// Calculates the difference between two <see cref="DateTime"/> values and returns the result as a <see cref="DateSpan"/>.
    /// </summary>
    /// <param name="first">The first <see cref="DateTime"/> value in the calculation.</param>
    /// <param name="second">The second <see cref="DateTime"/> value in the calculation.</param>
    /// <returns>A <see cref="DateSpan"/> representing the years, months, and days difference between the two <see cref="DateTime"/> values.</returns>
    public static DateSpan Difference(DateTime first, DateTime second)
    {
        long ticks = first.Ticks >= second.Ticks ? first.Ticks - second.Ticks : second.Ticks - first.Ticks;
        decimal days = (decimal)ticks / TimeSpan.TicksPerDay;

        return FromDays(days);
    }
}