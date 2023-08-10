using UnityEngine;

namespace Morchul.Utility.SaveLoad
{
	public abstract class SaveHandler : ScriptableObject, ISaveHandler
	{
		public abstract void SaveGameStatus(ISaveFileWriter writer);

		public abstract void LoadGameStatus(ISaveFileReader reader);

		public abstract void AfterLoad();
	}
}

