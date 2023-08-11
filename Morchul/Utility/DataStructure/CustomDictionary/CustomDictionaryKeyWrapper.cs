namespace Morchul.Utility
{
	public struct CustomDictionaryKeyWrapper<K, V> where V: new() where K : ICustomDictionaryKey
	{
		public bool Valid => index >= 0;
		private readonly int index;
		private readonly string identifier;

		public CustomDictionaryKeyWrapper(int index, string identifier)
		{
			this.identifier = identifier;
			this.index = index;
		}

		public void SetValue(V value, CustomDictionary<K, V> container)
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

		public V GetValue(CustomDictionary<K, V> container)
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

		/*public void SetKey(K keys, CustomDictionary<K, V> container)
		{
			if (container.Identifier == identifier)
			{
				this.index = container.GetIndexOf(keys);
			}
			else
			{
				throw new System.Exception("Different Identifier can't use this wrapper on.");
			}
		}*/
	}
}

