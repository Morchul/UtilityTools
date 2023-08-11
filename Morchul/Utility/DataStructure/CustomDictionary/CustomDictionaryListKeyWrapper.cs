using System.Collections.Generic;

namespace Morchul.Utility
{
	public struct CustomDictionaryListKeyWrapper<K, V> where K : ICustomDictionaryKey //<K, V> : CustomDictionaryKeyWrapper<K, List<V>> where K : ICustomDictionaryKey
	{
		public bool Valid => index >= 0;
		private readonly int index;
		private readonly string identifier;

		public CustomDictionaryListKeyWrapper(int index, string identifier)
		{
			this.identifier = identifier;
			this.index = index;
		}

		public void SetValue(List<V> value, CustomDictionary<K, List<V>> container)
		{
			if (container.Identifier == identifier)
			{
				container.Values[index] = value;
			}
			else
			{
				throw new System.Exception("Different Identifier can't use this wrapper on.");
			}
		}

		public List<V> GetValue(CustomDictionary<K, List<V>> container)
		{
			if (container.Identifier == identifier)
			{
				return container.Values[index];
			}
			else
			{
				throw new System.Exception("Different Identifier can't use this wrapper on.");
			}
		}

		public void AddValue(V value, CustomDictionaryList<K, V> container)
		{
			container.Values[index].Add(value);
		}

		public void RemoveValue(V value, CustomDictionaryList<K, V> container)
		{
			container.Values[index].Remove(value);
		}

		public int GetCount(CustomDictionaryList<K, V> container)
		{
			return container.Values[index].Count;
		}
	}
}

