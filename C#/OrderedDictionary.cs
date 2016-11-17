/**
 * Generic Implementation of OrderedDictionary from .NET
 *
 * The order in which the items are returned during an enumeration is the same as insertion order.
 *
 * Copyright (c) 2016 Leandro F. Vieira (https://github.com/leandromoh/)
 * Free under terms of the MIT license: http://www.opensource.org/licenses/mit-license.php
 *
 */

namespace System.Collections.Generic
{
    public class OrderedDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IReadOnlyCollection<KeyValuePair<TKey, TValue>>
    {
        private Dictionary<TKey, TValue> dic;
        private ICollection<KeyValuePair<TKey, TValue>> col;
        private HashSet<TKey> keysSet;
        private List<TKey> keys;

        public OrderedDictionary()
        {
            dic = new Dictionary<TKey, TValue>();
            AfterConstructor();
        }

        public OrderedDictionary(int capacity)
        {
            dic = new Dictionary<TKey, TValue>(capacity);
            AfterConstructor();
        }

        public OrderedDictionary(IEqualityComparer<TKey> comparer)
        {
            dic = new Dictionary<TKey, TValue>(comparer);
            AfterConstructor();
        }

        public OrderedDictionary(int capacity, IEqualityComparer<TKey> comparer)
        {
            dic = new Dictionary<TKey, TValue>(capacity, comparer);
            AfterConstructor();
        }

        private void AfterConstructor()
        {
            col = dic;
            keys = new List<TKey>();
            keysSet = new HashSet<TKey>(dic.Comparer);
        }

        // IDictionary<TKey, TValue> Implementation 

        public TValue this[TKey key]
        {
            get
            {
                return dic[key];
            }

            set
            {
                if (keysSet.Add(key))
                {
                    keys.Add(key);
                }

                dic[key] = value;
            }
        }

        public ICollection<TKey> Keys
        {
            get
            {
                return dic.Keys;
            }
        }

        public ICollection<TValue> Values
        {
            get
            {
                return dic.Values;
            }
        }

        public void Add(TKey key, TValue value)
        {
            dic.Add(key, value);
            keysSet.Add(key);
            keys.Add(key);
        }

        public bool ContainsKey(TKey key)
        {
            return dic.ContainsKey(key);
        }

        public bool Remove(TKey key)
        {
            if (dic.Remove(key))
            {
                keys.Remove(key);
                keysSet.Remove(key);
                return true;
            }
            return false;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return dic.TryGetValue(key, out value);
        }

        // ICollection<T> Implementation

        public int Count
        {
            get
            {
                return col.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return col.IsReadOnly;
            }
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        public void Clear()
        {
            col.Clear();
            keys.Clear();
            keysSet.Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return col.Contains(item);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            col.CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            if (col.Remove(item))
            {
                keys.Remove(item.Key);
                keysSet.Remove(item.Key);
                return true;
            }
            return false;
        }

        // IEnumerable<T> Implementation

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            foreach (var k in keys)
                yield return new KeyValuePair<TKey, TValue>(k, dic[k]);
        }

        // IEnumerable Implementation

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
