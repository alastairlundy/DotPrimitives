using AlastairLundy.Resyslib.Collections.Extensions.Generic.GenericArrayLists;
using AlastairLundy.Resyslib.Collections.Generics.ArrayLists;

using Bogus;

namespace Resyslib.Collections.Benchmarks.Infra;

public class FakeStringEnumerables
{
    private readonly Faker _faker;

    public FakeStringEnumerables()
    {
        _faker = new Faker();
    }

    public IEnumerable<KeyValuePair<int, string>> CreateKeyValuePairEnumerable(int count)
    {
        List<KeyValuePair<int, string>> list = new List<KeyValuePair<int, string>>();

        for (int i = 0; i < count; i++)
        {
            list.Add(new KeyValuePair<int, string>(i, _faker.Address.FullAddress()));
        }

        return list;
    }

    public List<string> CreateList(int count)
    {
        IList<string> list = _faker.Make<string>(count, _faker.Address.FullAddress);

        return list.ToList();
    }
    
    public Dictionary<int, string> CreateDictionary(int count)
    {
        Dictionary<int, string> dictionary = new Dictionary<int, string>();

        for (int i = 0; i < count; i++)
        {
            dictionary.Add(i, _faker.Address.FullAddress());
        }

        return dictionary;
    }

    public GenericArrayList<string> CreateGenericArrayList(int count)
    {
        IList<string> list = _faker.Make<string>(count, _faker.Address.FullAddress);

        return list.ToGenericArrayList();
    }
}