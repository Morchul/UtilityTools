namespace Morchul.Utility.SaveLoad
{
	public abstract class SimpleSaveHandler<T> : SaveHandler
	{
		public override void SaveGameStatus(ISaveFileWriter writer)
		{
			ISaveable<T> saveable = GetSaveable();

			if (saveable.Dirty)
			{
				SaveData(writer, saveable.GetSaveData());
			}
			else
			{
				writer.Write(SaveLoadManager.CONTROL_SIGN.END);
			}
		}

		public override void LoadGameStatus(ISaveFileReader reader)
		{
			ISaveable<T> saveable = GetSaveable();

			if(reader.PeekControlSign() != SaveLoadManager.CONTROL_SIGN.END)
			{
				saveable.DataLoaded(LoadData(reader));
			}
		}

		public abstract void SaveData(ISaveFileWriter writer, T saveData);
		public abstract T LoadData(ISaveFileReader reader);
		public abstract ISaveable<T> GetSaveable();
	}

	public abstract class EnhancedSaveHandler<T> : SaveHandler
	{
		public override void SaveGameStatus(ISaveFileWriter writer)
		{
			foreach (IIdentifyableSaveable<T> saveable in GetSaveables())
			{
				if (saveable.Dirty)
				{
					writer.WriteID(saveable);
					SaveData(writer, saveable.GetSaveData());
				}
			}

			writer.Write(SaveLoadManager.CONTROL_SIGN.END);
		}

		public override void LoadGameStatus(ISaveFileReader reader)
		{
			IIdentifyableSaveable<T>[] saveables = GetSaveables();

			int ID;
			while ((ID = reader.ReadInt()) != (int)SaveLoadManager.CONTROL_SIGN.END)
			{
				T data = LoadData(reader);

				foreach (IIdentifyableSaveable<T> saveable in saveables)
				{
					if (saveable.ID == ID)
					{
						saveable.DataLoaded(data);
						break;
					}
				}
			}
		}

		public abstract void SaveData(ISaveFileWriter binaryWriter, T saveData);
		public abstract T LoadData(ISaveFileReader reader);
		public abstract IIdentifyableSaveable<T>[] GetSaveables();
	}
}

