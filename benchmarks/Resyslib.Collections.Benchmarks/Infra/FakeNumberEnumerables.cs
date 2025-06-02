using Bogus;

namespace Resyslib.Collections.Benchmarks.Infra;

public class FakeNumberEnumerables
{
    private readonly Faker _faker;

    public FakeNumberEnumerables()
    {
        _faker = new Faker();
    }
    
    public List<int> CreateList(int count)
    {
        List<int> list = new List<int>();

        for (int i = 0; i < count; i++)
        {
            list.Add(_faker.Random.Int());   
        }

        return list;
    }

}