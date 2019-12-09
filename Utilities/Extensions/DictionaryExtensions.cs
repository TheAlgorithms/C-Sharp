using System.Collections.Generic;

namespace Utilities.Extensions
{
    public static class DictionaryExtensions
    {
        public static void AddMany<TKey, TValue>(this Dictionary<TKey, TValue> keys, IEnumerable<(TKey, TValue)> enumerable)
        {
            foreach (var (key, value) in enumerable)
            {
                keys.Add(key, value);
            }
        }
    }
}
