using AlastairLundy.Resyslib.Collections.Generics.ArrayLists;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Order;
using Resyslib.Collections.Benchmarks.Infra;

namespace Resyslib.Collections.Benchmarks.Generics.ArrayLists;

//[SimpleJob(RuntimeMoniker.Net80)]
[SimpleJob(RuntimeMoniker.Net90)]
[MemoryDiagnoser(true), Orderer(SummaryOrderPolicy.FastestToSlowest)]
[CsvMeasurementsExporter]
public class GenericArrayListAdditionBenchmark
{
    private FakeStringEnumerables fakeStringEnumerables;
    private FakeNumberEnumerables fakeNumberEnumerables;

    private IList<string> fakeData1;
    private IList<int> fakeData2;

    public GenericArrayListAdditionBenchmark()
    {
        fakeStringEnumerables = new FakeStringEnumerables();
        fakeNumberEnumerables = new FakeNumberEnumerables();
    }
    
    [GlobalSetup]
    public void Setup()
    {
        fakeData1 = fakeStringEnumerables.CreateList(N);
        fakeData2 = fakeNumberEnumerables.CreateList(N);
    }
    
    [Params(
        1000
        //,10_000
        //,
        //   100_000
        //,
        //1_000_000
        //,10_000_000
    )]
    public int N;

    [Benchmark]
    public void List_String()
    {
        List<string> list = new List<string>();

        foreach (string item in fakeData1)
        {
            list.Add(item);
        }

        Console.WriteLine(list.Count);
    }
    
    [Benchmark]
    public void List_Int()
    {
        List<int> list = new List<int>();

        foreach (int item in fakeData2)
        {
            list.Add(item);
        }

        Console.WriteLine(list.Count);
    }
    
    
    [Benchmark]
    public void GenericArrayList_String()
    {
        GenericArrayList<string> list = new GenericArrayList<string>();

        foreach (string item in fakeData1)
        {
            list.Add(item);
        }

        Console.WriteLine(list.Count);
    }
    
    [Benchmark]
    public void GenericArrayList_Int()
    {
        GenericArrayList<int> list = new GenericArrayList<int>();

        foreach (int item in fakeData2)
        {
            list.Add(item);
        }

        Console.WriteLine(list.Count);
    }
}