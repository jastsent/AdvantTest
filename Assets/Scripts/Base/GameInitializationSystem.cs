using AdvantTest.Configs;
using AdvantTest.Save;
using AdvantTest.Shop;
using Leopotam.EcsLite;

namespace AdvantTest.Base
{
    public sealed class GameInitializationSystem : IEcsInitSystem
    {
        private readonly Config _config;
        private readonly SaveData _saveData;

        public GameInitializationSystem(Config config, SaveData saveData)
        {
            _config = config;
            _saveData = saveData;
        }
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            
            var newEntity = world.NewEntity();
            var balancePool = world.GetPool<BalanceComponent>();
            ref var balanceComponent = ref balancePool.Add(newEntity);
            balanceComponent.SetSaveData(_saveData);
            
            var shopPool = world.GetPool<ShopComponent>();
            ref var shopsData = ref _saveData.ShopsData;
            
            for (var i = 0; i < _config.ShopConfigs.Count; i++)
            {
                var shopConfig = _config.ShopConfigs[i];
                newEntity = world.NewEntity();
                ref var shopComponent = ref shopPool.Add(newEntity);
                shopComponent.SetConfig(shopConfig);
                shopComponent.SetSaveData(shopsData[i]);
            }
        }
    }
}