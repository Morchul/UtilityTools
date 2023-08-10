using UnityEngine;

namespace Morchul.Utility.SaveLoad
{
    [CreateAssetMenu(fileName = "SaveCluster", menuName = "SaveLoad/SaveCluster")]
    public class SaveCluster : ScriptableObject
    {
        public string Name;
        public SaveHandler[] SaveHandlers;

        [Header("Progress")]
        public int StartProgress;
        public int EndProgress;

        public void Save(ISaveFileWriter writer)
        {
            foreach (ISaveHandler saveHandler in SaveHandlers)
            {
                saveHandler.SaveGameStatus(writer);
            }
        }

        public void Load(ISaveFileReader reader)
        {
            foreach (ISaveHandler saveHandler in SaveHandlers)
            {
                saveHandler.LoadGameStatus(reader);
            }
            foreach (ISaveHandler saveHandler in SaveHandlers)
            {
                saveHandler.AfterLoad();
            }
        }
    }
}

