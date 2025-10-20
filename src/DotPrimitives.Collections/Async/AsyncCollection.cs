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

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AlastairLundy.DotPrimitives.Collections.Async;

public class AsyncCollection<T> : IAsyncCollection<T>
{
    private IAsyncEnumerable<T> _enumerable;
    
    public AsyncCollection()
    {
        _count = 0;
        _enumerable = AsyncEnumerable<T>.Empty;
    }
    
    private int _count;
    
    public async IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = new CancellationToken())
    {
        await foreach (var item in _enumerable.WithCancellation(cancellationToken))
        {
            yield return item;
        }
    }

    public int Count =>  _count;
    
    
    public async Task<bool> AddAsync(T item)
    {
         _enumerable = _enumerable.
        
        bool success = false;
        

        if (success)
            _count += 1;
    }

    
    public async Task<bool> RemoveAsync(T item)
    {
        bool success = false;



        if (success)
            _count -= 1;

        
    }

    
    public async Task<bool> Clear()
    {
        
        _count = 0;
    }
}