using System;
using DotPrimitives.Dates;
using Xunit;

namespace DotPrimitives.Tests.Dates;

public class DateSpanTests
{
    [Fact]
    public void FromDays_ToTimeSpan_Roundtrip()
    {
        decimal days = decimal.Parse("5.5");
        DateSpan ds = DateSpan.FromDays(days);
        Assert.Equal(days, ds.TotalDays, 8);
        TimeSpan ts = ds.ToTimeSpan();
        Assert.Equal(days, (decimal)ts.TotalDays, 8);
    }

    [Fact]
    public void MultiplyOperator_ScalesDays()
    {
        DateSpan ds = DateSpan.FromDays(10.0);
        DateSpan scaled1 = ds * 2.0;
        DateSpan scaled2 = 3.0 * ds;
        Assert.Equal(20, scaled1.TotalDays, 8);
        Assert.Equal(30, scaled2.TotalDays, 8);
    }

    [Fact]
    public void AddSubtractOperators_WorkOnTotalDays()
    {
        var a = DateSpan.FromDays(5.0);
        var b = DateSpan.FromDays(3.0);
        var sum = a + b;
        var diff = a - b;
        Assert.Equal(8, sum.TotalDays, 8);
        Assert.Equal(2, diff.TotalDays, 8);
    }

    [Fact]
    public void EqualityAndHashCode_ForSameValues()
    {
        var a = DateSpan.FromDays(7.0);
        var b = DateSpan.FromDays(7.0);
        Assert.True(a == b);
        Assert.False(a != b);
        Assert.Equal(a, b);
        Assert.Equal(a.GetHashCode(), b.GetHashCode());
    }

    [Fact]
    public void ComparisonOperators_BehaveAsExpected()
    {
        var small = DateSpan.FromDays(1.0);
        var large = DateSpan.FromDays(2.0);
        Assert.True(small < large);
        Assert.True(large > small);
        Assert.True(large >= small);
        Assert.Equal(-1, small.CompareTo(large));
        Assert.Equal(1, large.CompareTo(small));
        Assert.Equal(0, small.CompareTo(small));
    }

    [Fact]
    public void ToString_DefaultFormat_IsNotEmpty()
    {
        var ds = DateSpan.FromDays(2.0);
        var str = ds.ToString();
        Assert.False(string.IsNullOrWhiteSpace(str));
        Assert.Contains(":", str); // class formats as years:months:days
    }

    [Fact]
    public void Parse_NullOrEmpty_ThrowsArgumentNullException()
    {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        Assert.Throws<ArgumentNullException>(() => DateSpan.Parse(null));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        Assert.Throws<ArgumentNullException>(() => DateSpan.Parse(string.Empty));
    }

    [Fact]
    public void Parse_InvalidCharacters_ThrowsOverflowException()
    {
        Assert.Throws<OverflowException>(() => DateSpan.Parse("abc"));
    }

    [Fact]
    public void TryParse_NullOrEmpty_ReturnsFalse()
    {
        string? s = null;
        var ok = DateSpan.TryParse(s, null, out var result);
        Assert.False(ok);
        Assert.Equal(default(DateSpan), result);

        ok = DateSpan.TryParse(string.Empty, null, out result);
        Assert.False(ok);
        Assert.Equal(default(DateSpan), result);
    }

    [Fact]
    public void Parse_ReadOnlySpan_Invalid_ThrowsOverflowException()
    {
        Assert.Throws<OverflowException>(() => DateSpan.Parse("abc".AsSpan(), null));
    }

    [Fact]
    public void TryParse_ReadOnlySpan_Empty_ReturnsFalse()
    {
        ReadOnlySpan<char> empty = ReadOnlySpan<char>.Empty;
        var ok = DateSpan.TryParse(empty, null, out var result);
        Assert.False(ok);
        Assert.Equal(default(DateSpan), result);
    }
}