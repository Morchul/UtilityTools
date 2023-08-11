using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Morchul.Utility.SaveLoad
{
	[CreateAssetMenu(fileName = "SaveLoadManager", menuName = "SaveLoad/Manager")]
	public class SaveLoadManager : ScriptableObject
	{
		[Header("Settings")]
		[SerializeField]
		private string baseSaveDirectory;

		[Header("Save progress system")]
		[SerializeField]
		private SaveCluster[] saveClusters;

		private const string BASE_INFO_SAVE_FILE = "BaseInfo.txt";

		public List<SaveSlot> SaveSlots { get; private set; }

		private int currentSaveSlotIndex;
		public SaveSlot CurrentSaveSlot => SaveSlots[currentSaveSlotIndex];
		public bool IsNoSaveSlotSelected => currentSaveSlotIndex < 0;

		#region Writer / Reader
		private ISaveFileWriter writer;
		private ISaveFileWriter Writer
		{
			get
			{
				if (writer == null)
					writer = new SaveFileSimpleWriter();
				return writer;
			}
			set
			{
				writer = value;
			}
		}

		private ISaveFileReader reader;
		private ISaveFileReader Reader
		{
			get
			{
				if (reader == null)
					reader = new SaveFileSimpleReader();
				return reader;
			}
			set
			{
				reader = value;
			}
		}
		#endregion

		#region public
		public void Init()
		{
			if (!Directory.Exists(baseSaveDirectory))
			{
				Directory.CreateDirectory(baseSaveDirectory);
			}

			string[] saveSlots = Directory.GetDirectories(baseSaveDirectory);
			this.SaveSlots = new List<SaveSlot>(saveSlots.Length);
			currentSaveSlotIndex = -1;
			foreach (string directory in saveSlots)
			{
				LoadSaveSlot(new DirectoryInfo(directory).Name);
			}
		}

		public void SelectSaveSlot(SaveSlot saveSlot)
		{
			currentSaveSlotIndex = saveSlot.Index;
		}

		public void UpdateGameSlotBaseInfo()
		{
			if (IsNoSaveSlotSelected) return;
			CurrentSaveSlot.SaveDateTime = Utility.GetCurrentSaveDateTime();
			SaveBaseInformation(Writer, CurrentSaveSlot);
		}

		public void Save(int progress)
		{
			foreach (SaveCluster saveCluster in saveClusters)
			{
				if (progress >= saveCluster.StartProgress && progress <= saveCluster.EndProgress)
					Save(saveCluster);
			}

			UpdateGameSlotBaseInfo();
		}

		public void Load(int progress)
		{
			foreach (SaveCluster saveCluster in saveClusters)
			{
				if (progress >= saveCluster.StartProgress && progress <= saveCluster.EndProgress)
					Load(saveCluster);
			}
		}

		public void Save(SaveCluster saveCluster)
		{
			if (IsNoSaveSlotSelected) return;

			if (!CurrentSaveSlot.IsNew)
			{
				//Override warning
			}
			else
			{
				Directory.CreateDirectory(GetSaveDirectory(CurrentSaveSlot));
				SaveBaseInformation(Writer, CurrentSaveSlot);
			}

			try
			{
				Writer.OpenFile(GetSaveFile(CurrentSaveSlot, saveCluster));

				saveCluster.Save(Writer);

				Debug.Log("Successfully saved!");
			}
			catch (Exception e)
			{
				Debug.LogError("Exception while save gamestate to file: " + e.Message + e.StackTrace);
			}
			finally
			{
				Writer.CloseFile();
			}
		}

		public void Load(SaveCluster saveCluster)
		{
			if (IsNoSaveSlotSelected) return;

			try
			{
				Reader.OpenFile(GetSaveFile(CurrentSaveSlot, saveCluster));
				saveCluster.Load(Reader);

				Debug.Log("Successfully loaded!");
			}
			catch (Exception e)
			{
				Debug.LogError("Exception while load gamestate from file: " + e.Message + e.StackTrace);
			}
			finally
			{
				Reader.CloseFile();
			}

		}

		public SaveSlot CreateNewSaveSlot(string name)
		{
			SaveSlot saveSlot = new SaveSlot(SaveSlots.Count, name, true);
			SaveSlots.Add(saveSlot);
			return saveSlot;
		}

		public void LoadSaveSlot(string directory)
		{
			SaveSlot saveSlot = new SaveSlot(SaveSlots.Count, directory, false);
			LoadBaseInformation(Reader, saveSlot);
			SaveSlots.Add(saveSlot);
		}
		#endregion

		#region Base game information
		private void SaveBaseInformation(ISaveFileWriter writer, SaveSlot saveSlot)
		{
			writer.OpenFile(GetBaseSaveFile(saveSlot));
			saveSlot.Save(writer);
			writer.CloseFile();
		}

		private void LoadBaseInformation(ISaveFileReader reader, SaveSlot saveSlot)
		{
			reader.OpenFile(GetBaseSaveFile(saveSlot));
			saveSlot.Load(reader);
			reader.CloseFile();
		}
		#endregion

		#region Helper
		public string GetSaveDirectory(SaveSlot saveSlot)
		{
			return baseSaveDirectory + "\\" + saveSlot.Name;
		}

		public string GetSaveFile(SaveSlot saveSlot, SaveCluster saveCluster)
		{
			return GetSaveDirectory(saveSlot) + "\\" + saveCluster.Name;
		}

		public string GetBaseSaveFile(SaveSlot saveSlot)
		{
			return GetSaveDirectory(saveSlot) + "\\" + BASE_INFO_SAVE_FILE;
		}
		#endregion

		#region Control sign
		public const long CONTROL_SIGN_LENGTH = sizeof(CONTROL_SIGN); //in bytes
		public enum CONTROL_SIGN : int
		{
			NONE = -1,
			NULL = -2,
			END = -3
		}
		#endregion
	}
}
