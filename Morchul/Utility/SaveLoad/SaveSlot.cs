namespace Morchul.Utility.SaveLoad
{
    public class SaveSlot
    {
        public int Index { get; }
        public string Name { get; }
        public bool IsNew { get; private set; }

        public string SaveDateTime;
        public int Progress;

        public SaveSlot(int index, string name, bool _new)
        {
            Index = index;
            Name = name;
            IsNew = _new;
        }

        public virtual void Save(ISaveFileWriter writer)
        {
            IsNew = false;
            writer.Write(SaveDateTime);
            writer.Write(Progress);
        }

        public virtual void Load(ISaveFileReader reader)
        {
            SaveDateTime = reader.ReadString();
            Progress = reader.ReadInt();
        }
    }
}

