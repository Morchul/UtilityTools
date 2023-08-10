using UnityEngine;
using System.Collections;
using System.IO;

namespace Morchul.Utility.SaveLoad
{
	/// <summary>
	/// 
	/// </summary>
	public class SaveFileSimpleWriter : ISaveFileWriter
	{
		private StreamWriter writer;

		public void CloseFile()
		{
			if (writer == null) return;

			writer.Close();
			writer.Dispose();
			writer = null;
		}

		public void OpenFile(string file)
		{
			if (writer != null)
				CloseFile();
			
			writer = new StreamWriter(file);
		}

		public void Write(int data)
		{
			writer.WriteLine(data);
		}

		public void Write(float data)
		{
			writer.WriteLine(data);
		}

		public void Write(string data)
		{
			writer.WriteLine(data);
		}

		public void Write(bool data)
		{
			writer.WriteLine(data);
		}

		public void Write(SaveLoadManager.CONTROL_SIGN controlSign)
		{
			Write((int)controlSign);
		}

		public void WriteID(IBase data)
		{
			if (data == null)
				Write(SaveLoadManager.CONTROL_SIGN.NULL);
			else
				writer.WriteLine(data.ID);
		}

		public void Flush()
		{
			writer.Flush();
		}
	}
}

