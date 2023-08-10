namespace Morchul.Utility.SaveLoad
{
	/// <summary>
	/// 
	/// </summary>
	public interface ISaveFileReader : ISaveFileHandler
	{
		public int ReadInt();
		public float ReadFloat();
		public string ReadString();
		public bool ReadBool();

		public SaveLoadManager.CONTROL_SIGN ReadControlSign();
		public SaveLoadManager.CONTROL_SIGN PeekControlSign();
	}
}

