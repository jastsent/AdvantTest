using System.IO;
using UnityEngine;
using File = System.IO.File;

namespace AdvantTest.Save
{
    public sealed class JsonSaver : ISaver
    {
        public void Save<T>(T saveObject, string path) where T : class
        {
            var directoryName = Path.GetDirectoryName(path);
            if(!string.IsNullOrEmpty(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }
            
            var json = JsonUtility.ToJson(saveObject);
            File.WriteAllTextAsync(path, json);
        }
        
        public bool Load<T>(string path, out T loaded) where T : class, new()
        {
            if (IsFileExist(path))
            {
                var json = File.ReadAllText(path);
                loaded = JsonUtility.FromJson<T>(json);
                return true;
            }

            loaded = null;
            return false;
        }

        public bool IsFileExist(string path)
        {
            return File.Exists(path);
        }
    }
}