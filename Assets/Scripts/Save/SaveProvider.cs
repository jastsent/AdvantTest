using System;
using System.IO;
using UnityEngine;

namespace AdvantTest.Save
{
    public sealed class SaveProvider
    {
        public SaveData SaveData => _saveData;
        
        private const string FileName = "save.json";
        private string FilePath
        {
            get
            {
#if UNITY_EDITOR
                return Path.Combine(Application.dataPath, FileName);
#else
                return Path.Combine(Application.persistentDataPath, FileName);
#endif
            }
        }

        private readonly ISaver _saver;
        private SaveData _saveData;

        public SaveProvider(ISaver saver)
        {
            _saver = saver;
        }

        public SaveData Load(Func<SaveData> saveDataFabric)
        {
            if (!_saver.Load(FilePath, out _saveData))
            {
                _saveData = saveDataFabric();
            }

            return _saveData;
        }

        public void Save()
        {
            _saver.Save(_saveData, FilePath);
        }
    }
}