/**
 * Generic Implementation of OrderedDictionary from .NET
 *
 * The order in which the items are returned during a enumeration is the same as insertion order.
 *
 * Copyright (c) 2016 Leandro F. Vieira (https://github.com/leandromoh/)
 * Free under terms of the MIT license: http://www.opensource.org/licenses/mit-license.php
 *
 */

using System.Collections;
using System.Collections.Generic;

namespace ConsoleApplication1
{
    public class OrderedDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IReadOnlyCollection<KeyValuePair<TKey, TValue>>
    {
        private Dictionary<TKey, TValue> dic;
        private ICollection<KeyValuePair<TKey, TValue>> col;

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

        public OrderedDictionary(IDictionary<TKey, TValue> dictionary)
        {
            dic = new Dictionary<TKey, TValue>(dictionary);
            AfterConstructor();
        }

        public OrderedDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer)
        {
            dic = new Dictionary<TKey, TValue>(dictionary, comparer);
            AfterConstructor();
        }

        private void AfterConstructor()
        {
            col = dic;
            keys = new List<TKey>();
        }

        public TValue this[TKey key]
        {
            get
            {
                return dic[key];
            }

            set
            {
                if (keys.IndexOf(key) == -1)
                {
                    keys.Add(key);
                }

                dic[key] = value;
            }
        }

        public int Count
        {
            get
            {
                return dic.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return col.IsReadOnly;
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

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        public void Add(TKey key, TValue value)
        {
            dic.Add(key, value);
            keys.Add(key);
        }

        public void Clear()
        {
            dic.Clear();
            keys.Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return col.Contains(item);
        }

        public bool ContainsKey(TKey key)
        {
            return dic.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            col.CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            foreach (var k in keys)
                yield return new KeyValuePair<TKey, TValue>(k, dic[k]);
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            bool b = col.Remove(item);
            if (b) keys.Remove(item.Key);
            return b;
        }

        public bool Remove(TKey key)
        {
            bool b = dic.Remove(key);
            if (b) keys.Remove(key);
            return b;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return dic.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
