using System.IO;

namespace Morchul.Utility.SaveLoad
{
	/// <summary>
	/// 
	/// </summary>
	public interface ISaveFileHandler
	{
		public void OpenFile(string file);
		public void CloseFile();
	}
}

