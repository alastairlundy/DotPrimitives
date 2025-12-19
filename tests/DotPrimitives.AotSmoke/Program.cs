using DotPrimitives.AotSmoke.Localizations;
using DotPrimitives.Collections.Groupings;
using DotPrimitives.Dates;
using DotPrimitives.Text;

// ReSharper disable LocalizableElement

// Minimal AoT smoke test: exercise a few APIs to ensure IL is rooted under trimming/AoT
// This is not a full test suite; it just references and uses types from the libraries.

Console.WriteLine(Resources.DotPrimitives_AoT_Messages_Intro);

// Exercise DateSpan API
var now = DateTime.UtcNow;
var later = now.AddDays(5).AddHours(30);
DateSpan span = DateSpan.Difference(now, later);
Console.WriteLine($"DateSpan TotalHours: {span.TotalDays}");

// Exercise LineEndingDetector/Format
string sample = "hello\r\nworld\r\n";
LineEndingFormat format = sample.GetLineEndingFormat();
Console.WriteLine($"Detected line ending: {format}");

// Exercise GroupingEnumerable from Collections
var items = new List<(string Key, int Value)>
{
    ("a", 1), ("a", 1), ("b", 3)
};

var groupAItems = items.Where(i => i.Key == "a").Select(i => i.Value);
var grouping = new GroupingEnumerable<string, int>("a", groupAItems);
int sum = 0;
foreach (var v in grouping) sum += v;
Console.WriteLine($"Grouping '{grouping.Key}' sum: {sum}");

Console.WriteLine("DotPrimitives AoT smoke test completed.");
