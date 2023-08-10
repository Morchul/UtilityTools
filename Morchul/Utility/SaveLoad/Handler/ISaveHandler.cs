namespace Morchul.Utility.SaveLoad
{
	public interface ISaveHandler
	{
		public void SaveGameStatus(ISaveFileWriter writer);
		public void LoadGameStatus(ISaveFileReader reader);

		public void AfterLoad();
	}
}

