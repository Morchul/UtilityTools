using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;

namespace Morchul.Utility.SaveLoad
{
	/// <summary>
	/// 
	/// </summary>
	public class SaveFileBinaryReader : ISaveFileReader
	{
		private BinaryReader reader;
		private Stream stream;

		public void OpenFile(string file)
		{
			if (stream != null)
				CloseFile();

			stream = File.Open(file, FileMode.Open);
			reader = new BinaryReader(stream, Encoding.UTF8, false);
		}

		public void CloseFile()
		{
			if (reader == null) return;

			reader.Close();
			stream.Close();

			reader.Dispose();
			stream.Dispose();

			reader = null;
			stream = null;
		}

		public int ReadInt() => reader.ReadInt32();

		public float ReadFloat() => reader.ReadSingle();

		public string ReadString() => reader.ReadString();

		public bool ReadBool() => reader.ReadBoolean();

		public SaveLoadManager.CONTROL_SIGN ReadControlSign()
		{
			return (SaveLoadManager.CONTROL_SIGN)reader.ReadInt32();
		}

		public SaveLoadManager.CONTROL_SIGN PeekControlSign()
		{
			int res = reader.ReadInt32();
			reader.BaseStream.Position -= SaveLoadManager.CONTROL_SIGN_LENGTH;
			return (SaveLoadManager.CONTROL_SIGN) res;
		}
	}
}

