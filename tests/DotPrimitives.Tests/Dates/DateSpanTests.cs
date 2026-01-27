using DotPrimitives.Dates;

namespace DotPrimitives.Tests.Dates;

public class DateSpanTests
{
    [Test]
    public async Task FromDays_ToTimeSpan_Roundtrip()
    {
        decimal days = decimal.Parse("5.5");
        DateSpan ds = DateSpan.FromDays(days);

        await Assert.That(days)
            .IsEqualTo(ds.TotalDays);
    }

    [Test]
    public async Task MultiplyOperator_ScalesDays()
    {
        DateSpan ds = DateSpan.FromDays(10.0);
        DateSpan scaled1 = ds * 2.0;
        DateSpan scaled2 = 3.0 * ds;

        await Assert.That(scaled1.TotalDays)
            .IsEqualTo(20);

        await Assert.That(scaled2.TotalDays)
            .IsEqualTo(30);
    }

    [Test]
    public async Task AddSubtractOperators_WorkOnTotalDays()
    {
        var a = DateSpan.FromDays(5.0);
        var b = DateSpan.FromDays(3.0);
        var sum = a + b;
        var diff = a - b;

        await Assert.That(sum.TotalDays)
            .IsEqualTo(8);
        
        await Assert.That(diff.TotalDays)
            .IsEqualTo(2);
    }

    [Test]
    public async Task EqualityAndHashCode_ForSameValues()
    {
        DateSpan a = DateSpan.FromDays(7.0);
        DateSpan b = DateSpan.FromDays(7.0);
        
        await Assert.That(a)
            .IsEqualTo(b);
    }

    [Test]
    public async Task ComparisonOperators_BehaveAsExpected()
    {
        var small = DateSpan.FromDays(1.0);
        var large = DateSpan.FromDays(2.0);
        
        await Assert.That(small < large)
            .IsTrue();
        
        await Assert.That(large > small).IsTrue();
        await Assert.That(large >= small).IsTrue();
        await Assert.That(small.CompareTo(large) == -1).IsTrue();
        
        await Assert.That(large.CompareTo(small) == 1).IsTrue();
        await Assert.That(small.CompareTo(small) == 0).IsTrue();
    }

    [Test]
    public async Task ToString_DefaultFormat_IsNotEmpty()
    {
        var ds = DateSpan.FromDays(2.0);
        var str = ds.ToString();
        
        await Assert.That(string.IsNullOrWhiteSpace(str)).
            IsFalse();
       
        await Assert.That($"{ds.TotalYears}:{ds.TotalMonths}:{ds.TotalDays}")
            .IsEqualTo(str); // class formats as years:months:days
    }

    [Test]
    public async Task Parse_NullOrEmpty_ThrowsArgumentNullException()
    {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        await Assert.ThrowsAsync<ArgumentException>(() => Task.FromResult(DateSpan.Parse(null)));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        await Assert.ThrowsAsync<ArgumentException>(() => Task.FromResult(DateSpan.Parse(string.Empty)));
    }

    [Test]
    public async Task Parse_InvalidCharacters_ThrowsOverflowException()
    {
        await Assert.ThrowsAsync<OverflowException>(() => Task.FromResult( DateSpan.Parse("abc")));
    }

    [Test]
    public async Task TryParse_NullOrEmpty_ReturnsFalse()
    {
        string? s = null;
        var ok = DateSpan.TryParse(s, null, out DateSpan result);
        
        await Assert.That(ok)
            .IsFalse();
        
        await Assert.That(default(DateSpan))
            .IsEqualTo(result);

        ok = DateSpan.TryParse(string.Empty, null, out result);
       
        await Assert.That(ok).IsFalse();
        
        await Assert.That(default(DateSpan))
            .IsEqualTo(result);
    }

    [Test]
    public async Task Parse_ReadOnlySpan_Invalid_ThrowsOverflowException()
    {
        await Assert.ThrowsAsync<OverflowException>(() => Task.FromResult(DateSpan.Parse("abc".AsSpan(), null)));
    }

    [Test]
    public async Task TryParse_ReadOnlySpan_Empty_ReturnsFalse()
    {
        ReadOnlySpan<char> empty = ReadOnlySpan<char>.Empty;
        bool ok = DateSpan.TryParse(empty, null, out DateSpan result);
        
        await Assert.That(ok)
            .IsFalse();
        
        await Assert.That(default(DateSpan))
            .IsEqualTo(result);
    }
}