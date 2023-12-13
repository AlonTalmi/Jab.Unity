using System.Collections.Generic;

namespace Jab.Unity.Extensions
{
    internal static class EnumerableExt
    {
        internal static IEnumerable<T> Yield<T>(this T item)
        {
            yield return item;
        }
    }
}