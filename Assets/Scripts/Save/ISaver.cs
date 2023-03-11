namespace AdvantTest.Save
{
    public interface ISaver
    {
        public void Save<T>(T saveObject, string path) where T : class;
        public bool Load<T>(string path, out T loaded) where T : class, new();
        public bool IsFileExist(string path);
    }
}