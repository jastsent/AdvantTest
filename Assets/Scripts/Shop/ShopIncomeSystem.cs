using AdvantTest.Base;
using AdvantTest.Utils;
using Leopotam.EcsLite;

namespace AdvantTest.Shop
{
    public sealed class ShopIncomeSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _shopFilter;
        private EcsPool<ShopComponent> _shopPool;
        private EcsFilter _balanceFilter;
        private EcsPool<BalanceComponent> _balancePool;

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            _shopFilter = world.Filter<ShopComponent>().End();
            _shopPool = world.GetPool<ShopComponent>();
            _balanceFilter = world.Filter<BalanceComponent>().End();
            _balancePool = world.GetPool<BalanceComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            var balanceEntity = _balanceFilter.GetFirstEntity();
            ref var balanceComponent = ref _balancePool.Get(balanceEntity);
            
            foreach (var entity in _shopFilter)
            {
                ref var shopComponent = ref _shopPool.Get(entity);
                if (shopComponent.Level > 0)
                {
                    shopComponent.IncomeTimer += UltraTime.deltaTime;
                    while (shopComponent.IncomeTimer >= shopComponent.IncomeDelay)
                    {
                        balanceComponent.Balance += shopComponent.CurrentIncome;
                        shopComponent.IncomeTimer -= shopComponent.IncomeDelay;
                    }
                }
            }
        }
    }
}