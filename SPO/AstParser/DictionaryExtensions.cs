namespace AstParser;

public static class DictionaryExtensions
{
    public static Dictionary<TValue, List<TKey>> Reverse<TValue, TKey>(this Dictionary<TKey, List<TValue>> dictionary)
        where TKey : notnull
        where TValue : notnull
    {
        Dictionary<TValue, List<TKey>> newDictionary = new Dictionary<TValue, List<TKey>>();
        foreach (KeyValuePair<TKey,List<TValue>> kvp in dictionary)
        {
            foreach (TValue value in kvp.Value)
            {
                if (!newDictionary.TryGetValue(value, out List<TKey>? keys))
                {
                    keys = new List<TKey>();
                    newDictionary.Add(value, keys);
                }
                keys.Add(kvp.Key);
            }
        }

        return newDictionary;
    }
}