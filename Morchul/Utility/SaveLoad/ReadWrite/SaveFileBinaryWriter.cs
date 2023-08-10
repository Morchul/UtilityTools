using System.IO;
using System.Text;

namespace Morchul.Utility.SaveLoad
{
	public class SaveFileBinaryWriter : ISaveFileWriter
	{
		private BinaryWriter writer;
		private Stream stream;

		public void OpenFile(string file)
		{
			if (stream != null)
				CloseFile();

			stream = File.Open(file, FileMode.Create);
			writer = new BinaryWriter(stream, Encoding.UTF8, false);
		}

		public void CloseFile()
		{
			if (writer == null) return;

			writer.Close();
			stream.Close();

			writer.Dispose();
			stream.Dispose();

			writer = null;
			stream = null;
		}

		public void Write(int data)
		{
			writer.Write(data);
		}

		public void Write(float data)
		{
			writer.Write(data);
		}

		public void Write(string data)
		{
			writer.Write(data);
		}

		public void Write(bool data)
		{
			writer.Write(data);
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
				writer.Write(data.ID);
		}

		public void Flush()
		{
			writer.Flush();
		}
	}
}

