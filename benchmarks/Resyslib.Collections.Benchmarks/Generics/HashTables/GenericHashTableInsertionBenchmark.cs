using AlastairLundy.Resyslib.Collections.Generics.HashTables;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Order;
using Resyslib.Collections.Benchmarks.Infra;

namespace Resyslib.Collections.Benchmarks.Generics.HashTables;

//[SimpleJob(RuntimeMoniker.Net80)]
[SimpleJob(RuntimeMoniker.Net90)]
[MemoryDiagnoser(true), Orderer(SummaryOrderPolicy.FastestToSlowest)]
[CsvMeasurementsExporter]
public class GenericHashTableInsertionBenchmark
{
    private FakeStringEnumerables fakeStringEnumerables;

    private IEnumerable<KeyValuePair<int, string>> fakeData1;
    private IEnumerable<KeyValuePair<int, string>> fakeData2;

    public GenericHashTableInsertionBenchmark()
    {
        fakeStringEnumerables = new FakeStringEnumerables();
    }
 
    [GlobalSetup]
    public void Setup()
    {
        fakeData1 = fakeStringEnumerables.CreateKeyValuePairEnumerable(N);
        fakeData2 = fakeStringEnumerables.CreateKeyValuePairEnumerable(N);
    }
    
    [Params(
        10_000
        //,
     //   100_000
        //,
        //1_000_000
        //,10_000_000
        )]
    public int N;

    [Benchmark]
    public void Dictionary()
    {
        Dictionary<int, string> dictionary = new Dictionary<int, string>();

        foreach (KeyValuePair<int, string> kvp in fakeData1)
        {
            dictionary.Add(kvp.Key, kvp.Value);
        }

        Console.WriteLine(dictionary.Count);
    }

    [Benchmark]
    public void GenericHashTable()
    {
        GenericHashTable<int, string> hashTable = new GenericHashTable<int,string>();

        foreach (KeyValuePair<int, string> kvp in fakeData2)
        {
            hashTable.Add(kvp.Key, kvp.Value);
        }

        Console.WriteLine(hashTable.Count);
    }
}