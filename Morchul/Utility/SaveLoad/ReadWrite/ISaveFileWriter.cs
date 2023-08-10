namespace Morchul.Utility.SaveLoad
{
	/// <summary>
	/// 
	/// </summary>
	public interface ISaveFileWriter : ISaveFileHandler
	{
		public void Write(int data);
		public void Write(float data);
		public void Write(string data);
		public void Write(bool data);
		public void Write(SaveLoadManager.CONTROL_SIGN controlSign);
		public void WriteID(IBase data);

		public void Flush();
	}
}

