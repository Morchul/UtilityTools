using System.Collections.Generic;

namespace Morchul.Utility
{
	public class CustomDictionaryList<K, V> : CustomDictionary<K, List<V>> where K : ICustomDictionaryKey
	{
		public CustomDictionaryList(K[] keys) : base(keys)
		{
		}

		public CustomDictionaryList(int size) : base(size)
		{
		}

		public void AddValueToGroup(V value, K key)
		{
			if (indexDictionary.TryGetValue(key.Identifier, out int index))
			{
				Values[index].Add(value);
			}
			else
			{
				throw new System.Exception("Can't find EntityGroup in EntityConatiner");
			}
		}

		public void RemoveValueFromGroup(V value, K key)
		{
			if (indexDictionary.TryGetValue(key.Identifier, out int index))
			{
				Values[index].Remove(value);
			}
			else
			{
				throw new System.Exception("Can't find EntityGroup in EntityConatiner");
			}
		}

		public List<V> GetValuesCopyOfGroups(K[] keys)
		{
			List<V> values = new List<V>();

			if (keys.Length == 1)
				values.AddRange(GetValueOfKey(keys[0]));

			else
			{
				foreach (K key in keys)
				{
					values.AddRange(GetValueOfKey(key));
				}
			}

			return values;
		}

		public int GetCount(K key)
		{
			if (indexDictionary.TryGetValue(key.Identifier, out int index))
			{
				return Values[index].Count;
			}
			else
			{
				throw new System.Exception("Can't find EntityGroup in EntityConatiner");
			}
		}

		public override void ClearKeyValues()
		{
			ClearValues();
			base.ClearKeyValues();
		}

		public void ClearValues()
		{
			for(int i = 0; i < this.Size; ++i)
			{
				Values[i].Clear();
			}
		}
		

		public new CustomDictionaryListKeyWrapper<K, V> UseKey(K key)
		{
			if (indexDictionary.TryGetValue(key.Identifier, out int index))
				return new CustomDictionaryListKeyWrapper<K, V>(index, Identifier);
			else
				return new CustomDictionaryListKeyWrapper<K, V>(-1, "");
		}
	}
}

