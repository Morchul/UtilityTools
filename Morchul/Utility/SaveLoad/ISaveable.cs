namespace Morchul.Utility.SaveLoad
{
	/// <summary>
	/// 
	/// </summary>
	public interface ISaveable<T>
	{
		public T GetSaveData();
		public void DataLoaded(T data);

		public bool Dirty { get; }
	}

	public interface IIdentifyableSaveable<T> : ISaveable<T>, IBase
	{
		
	}
}

