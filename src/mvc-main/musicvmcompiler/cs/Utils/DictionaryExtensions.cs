using System.Collections.Generic;

namespace musicvmcompiler.Utils
{
    public static class DictionaryExtensions
    {
        public static void AddAll<K, V>(this Dictionary<K, V> target, Dictionary<K, V> values)
        {
            foreach (var entry in values)
            {
                target.Add(entry.Key, entry.Value);
            }
        }
    }
}
