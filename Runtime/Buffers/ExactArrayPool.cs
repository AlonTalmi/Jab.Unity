using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Jab.UnityExtensions.Buffers
{
    public sealed class ExactArrayPool<T>
    {
        public static readonly ExactArrayPool<T> Shared = new();

        private readonly Dictionary<int, ConcurrentBag<T[]>> _buckets = new();

        public T[] Rent(int size)
        {
            if (!_buckets.TryGetValue(size, out var bucket))
            {
                bucket = new();
                _buckets.TryAdd(size, bucket);
            }
            
            return bucket.TryTake(out var array) ? array : new T[size];
        }

        public void Return(T[] array)
        {
            if (_buckets.TryGetValue(array.Length, out var bucket)) 
                bucket.Add(array);
        }
    }
}