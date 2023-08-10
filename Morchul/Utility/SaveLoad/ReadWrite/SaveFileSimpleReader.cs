using UnityEngine;
using System.Collections;
using System.IO;

namespace Morchul.Utility.SaveLoad
{
	/// <summary>
	/// 
	/// </summary>
	public class SaveFileSimpleReader : ISaveFileReader
	{
		private StreamReader reader;

		public void CloseFile()
		{
			if (reader == null) return;

			try
			{
				reader.Close();
				reader.Dispose();
				reader = null;
			}
			catch(IOException e)
			{
				Debug.LogError("Can't dispose StreamReader: " + e.Message);
			}
		}

		public void OpenFile(string file)
		{
			if (reader != null)
				CloseFile();

			reader = new StreamReader(file);
		}

		public bool ReadBool()
		{
			return ReadString() == "1";
		}

		public float ReadFloat()
		{
			return float.Parse(reader.ReadLine());
		}

		public int ReadInt()
		{
			return int.Parse(reader.ReadLine());
		}

		public string ReadString()
		{
			return reader.ReadLine();
		}

		public SaveLoadManager.CONTROL_SIGN ReadControlSign()
		{
			return (SaveLoadManager.CONTROL_SIGN) int.Parse(reader.ReadLine());
		}

		public SaveLoadManager.CONTROL_SIGN PeekControlSign()
		{
			return (SaveLoadManager.CONTROL_SIGN)reader.Peek();
		}
	}
}

