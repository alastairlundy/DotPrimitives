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
using System.Threading;

using AlastairLundy.DotPrimitives.Collections.Async.Extensions;

namespace AlastairLundy.DotPrimitives.Collections.Async;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public class AsyncEnumerable<T> : IAsyncEnumerable<T>
{
    private readonly IAsyncEnumerable<T> _enumerable;
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="enumerable"></param>
    public AsyncEnumerable(IAsyncEnumerable<T> enumerable)
    {
        _enumerable = enumerable;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="enumerable"></param>
    public AsyncEnumerable(IEnumerable<T> enumerable)
    {
        _enumerable = enumerable.ToAsyncEnumerable();
    }

    /// <summary>
    /// 
    /// </summary>
    public static IAsyncEnumerable<T> Empty 
        => new AsyncEnumerable<T>([]);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = new CancellationToken())
    {
        await using IAsyncEnumerator<T> enumerator = _enumerable.GetAsyncEnumerator(cancellationToken);

        while (await enumerator.MoveNextAsync())
        {
            yield return enumerator.Current;
        }
    }
}