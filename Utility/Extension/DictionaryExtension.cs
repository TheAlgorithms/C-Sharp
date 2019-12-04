using System.Collections.Generic;

namespace Utility.Extension
{
    public static class DictionaryExtension
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
