using System.Collections;
using System.Collections.Generic;

namespace Dabensymon.MultiDictionary
{
    public class TwoWayDictionary<TKey1, TKey2> : IEnumerable where TKey1: notnull where TKey2: notnull
    {
        private readonly IDictionary<TKey1, TKey2> _internalFirstDictionary = new Dictionary<TKey1, TKey2>();
        private readonly IDictionary<TKey2, TKey1> _internalSecondDictionary = new Dictionary<TKey2, TKey1>();

        public void Add(TKey1 item1, TKey2 item2)
        {
            _internalFirstDictionary.Add(item1, item2);
            _internalSecondDictionary.Add(item2, item1);
        }

        public ICollection<TKey1> FirstKeys => _internalFirstDictionary.Keys;
        public ICollection<TKey2> SecondKeys => _internalSecondDictionary.Keys;
        
        public bool ContainsFirstKey(object key) => _internalFirstDictionary.ContainsKey((TKey1)key);
        public bool ContainsSecondKey(object key) => _internalSecondDictionary.ContainsKey((TKey2)key);
        
        public bool ContainsKey(object key) => ContainsFirstKey(key) || ContainsSecondKey(key);
        
        public bool RemoveByFirstKey(TKey1 key)
        {
            var secondKey = _internalFirstDictionary[key];
            return _internalFirstDictionary.Remove(key) && _internalSecondDictionary.Remove(secondKey);
        }
        public bool RemoveBySecondKey(TKey2 key)
        {
            var firstKey = _internalSecondDictionary[key];
            return _internalSecondDictionary.Remove(key) && _internalFirstDictionary.Remove(firstKey);
        }
        public TKey2 GetValueByFirstKey(TKey1 key)
        {
            return _internalFirstDictionary[key];
        }

        public TKey1 GetValueBySecondKey(TKey2 key)
        { 
            return _internalSecondDictionary[key];
        }
        
        public bool TryGetValueByFirstKey(TKey1 key, out TKey2 value)
        {
            return _internalFirstDictionary.TryGetValue(key, out value);
        }

        public bool TryGetValueBySecondKey(TKey2 key, out TKey1 value)
        {
            return _internalSecondDictionary.TryGetValue(key, out value);
        }

        public IEnumerator<KeyValuePair<TKey1, TKey2>> GetEnumerator()
        {
            return _internalFirstDictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(KeyValuePair<TKey1, TKey2> item)
        {
            Add(item.Key, item.Value);
        }

        public void Clear()
        {
            _internalFirstDictionary.Clear();
            _internalSecondDictionary.Clear();
        }

        public bool Contains(KeyValuePair<TKey1, TKey2> item)
        {
            return _internalFirstDictionary.Contains(item);
        }

        public bool Remove(KeyValuePair<TKey1, TKey2> item)
        {
            return RemoveByFirstKey(item.Key);
        }

        public int Count => _internalFirstDictionary.Count;
    }
}