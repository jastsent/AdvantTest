using AdvantTest.Configs;
using AdvantTest.MainScreen;
using AdvantTest.Save;
using AdvantTest.Shop;
using Leopotam.EcsLite;
using UnityEngine;

namespace AdvantTest.Base
{
    public sealed class Game : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Content _content;
        [SerializeField] private Config _config;
        
        private EcsWorld _ecsWorld;
        private IEcsSystems _ecsSystems;
        private SaveProvider _saveProvider;

        private void Start()
        {
            DontDestroyOnLoad(this);

            Application.targetFrameRate = (int) Screen.currentResolution.refreshRateRatio.value;
            
            var saver = new JsonSaver();
            _saveProvider = new SaveProvider(saver);
            var saveData = _saveProvider.Load(InitNewSave);
            
            _ecsWorld = new EcsWorld();
            _ecsSystems = new EcsSystems(_ecsWorld);
            _ecsSystems
                .Add(new GameInitializationSystem(_config, saveData))
                .Add(new MainScreenViewSystem(_content, _canvas))
                .Add(new ShopInputSystem())
                .Add(new ShopIncomeSystem())
                .Add(new ShopViewSystem(_config, _content))
                .Init();
        }

        private SaveData InitNewSave()
        {
            var count = _config.ShopConfigs.Count;
            var saveData = new SaveData
            {
                ShopsData = new ShopData[count]
            };

            for (int i = 0; i < count; i++)
            {
                saveData.ShopsData[i] = new ShopData();
            }
            
            if (count > 0)
            {
                saveData.ShopsData[0].Level++;
            }

            return saveData;
        }

        private void Update()
        {
            _ecsSystems?.Run();
        }
        
        private void OnDestroy() {
            if (_ecsSystems != null) {
                _ecsSystems.Destroy();
                _ecsSystems = null;
            }
            
            if (_ecsWorld != null) {
                _ecsWorld.Destroy();
                _ecsWorld = null;
            }
        }

        private void OnApplicationPause(bool pause)
        {
            if(pause)
            {
                _saveProvider.Save();
            }
        }

        private void OnApplicationQuit()
        {
            _saveProvider.Save();
        }
    }
}
