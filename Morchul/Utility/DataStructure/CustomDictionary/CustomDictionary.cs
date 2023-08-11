using System.Collections.Generic;

namespace Morchul.Utility
{
	public class CustomDictionary<K, V> where K : ICustomDictionaryKey where V : new()
	{

		public readonly V[] Values;

		protected readonly Dictionary<string, int> indexDictionary;

		public readonly int Size;
		private int counter;

		public string Identifier { get; protected set; }

		public CustomDictionary(K[] keys, bool createDefaultValues = true): this(keys.Length)
		{
			for (int i = 0; i < keys.Length; ++i)
			{
				K key = keys[i];
				indexDictionary.Add(key.Identifier, i);
				if(createDefaultValues)
					CreateNewValue(i);
				Identifier += key.Identifier;
			}
		}

		public CustomDictionary(int size)
		{
			this.Size = size;
			Values = new V[this.Size];
			indexDictionary = new Dictionary<string, int>(this.Size);
			Identifier = "";
			counter = 0;
		}

		public void AddKey(K key, bool createDefaultValue = true)
		{
			if(counter < Size)
			{
				indexDictionary.Add(key.Identifier, counter);
				if (createDefaultValue)
					CreateNewValue(counter);
				Identifier += key.Identifier;
				++counter;
			}
			else
			{
				throw new System.Exception("EntityGroup is to small please create a bigger one");
			}
		}

		public void AddKeys(K[] keys, bool createDefaultValue = true)
		{
			foreach(K key in keys)
			{
				AddKey(key, createDefaultValue);
			}
		}

		public virtual void ClearKeyValues()
		{
			for(int i = 0; i < Size; ++i)
			{
				Values[i] = default;
			}

			indexDictionary.Clear();
			counter = 0;
			Identifier = "";
		}

		protected virtual void CreateNewValue(int index)
		{
			Values[index] = new V();
		}

		public void SetValueOfKey(V value, K key)
		{
			if (indexDictionary.TryGetValue(key.Identifier, out int index))
			{
				Values[index] = value;
			}
			else
			{
				throw new System.Exception("Can't find EntityGroup in EntityConatiner [1]");
			}
		}

		public V GetValueOfKey(K key)
		{
			if (indexDictionary.TryGetValue(key.Identifier, out int index))
			{
				return Values[index];
			}
			else
			{
				throw new System.Exception("Can't find EntityGroup in EntityConatiner [2]");
			}
		}

		public V[] GetValuesOfKeys(K[] keys)
		{
			if (keys.Length == 1)
				return new V[] { GetValueOfKey(keys[0]) };


			V[] values = new V[keys.Length];
			for (int i = 0; i < values.Length; ++i)
			{
				values[i] = GetValueOfKey(keys[i]);
			}
			return values;
		}

		public virtual CustomDictionaryKeyWrapper<K, V> UseKey(K keys)
		{
			if (indexDictionary.TryGetValue(keys.Identifier, out int index))
				return new CustomDictionaryKeyWrapper<K, V>(index, Identifier);
			else
				return new CustomDictionaryKeyWrapper<K, V>(-1, "");
		}

		public virtual int GetIndexOf(K key)
		{
			if(indexDictionary.TryGetValue(key.Identifier, out int value))
				return value;
			else
				throw new System.Exception("Can't find EntityGroup in EntityConatiner [3]");
		}
	}
}

