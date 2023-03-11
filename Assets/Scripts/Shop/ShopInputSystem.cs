using AdvantTest.Base;
using AdvantTest.Utils;
using Leopotam.EcsLite;

namespace AdvantTest.Shop
{
    public sealed class ShopInputSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _shopFilter;
        private EcsPool<ShopComponent> _shopPool;
        private EcsPool<ShopViewComponent> _shopViewPool;
        private EcsFilter _balanceFilter;
        private EcsPool<BalanceComponent> _balancePool;

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            _shopFilter = world.Filter<ShopComponent>().Inc<ShopViewComponent>().End();
            _shopPool = world.GetPool<ShopComponent>();
            _shopViewPool = world.GetPool<ShopViewComponent>();
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
                ref var shopViewComponent = ref _shopViewPool.Get(entity);
                var shopView = shopViewComponent.ShopView;

                if (shopView.LvlUpgradeButtonView.IsClicked)
                {
                    if (balanceComponent.Balance >= shopComponent.CurrentPrice)
                    {
                        balanceComponent.Balance -= shopComponent.CurrentPrice;
                        shopComponent.Level += 1;
                        shopComponent.IncomeProgress = 0f;
                        shopComponent.IsUpgradedEvent = true;
                    }
                }
                else if (shopView.FirstUpgradeButtonView.IsClicked 
                         && !shopComponent.IsFirstUpgradeAvailable 
                         && balanceComponent.Balance >= shopComponent.FirstUpgradePrice)
                {
                    balanceComponent.Balance -= shopComponent.FirstUpgradePrice;
                    shopComponent.IsFirstUpgradeAvailable = true;
                    shopComponent.IsUpgradedEvent = true;
                }
                else if (shopView.SecondUpgradeButtonView.IsClicked
                         && !shopComponent.IsSecondUpgradeAvailable
                         && balanceComponent.Balance >= shopComponent.SecondUpgradePrice)
                {
                    balanceComponent.Balance -= shopComponent.SecondUpgradePrice;
                    shopComponent.IsSecondUpgradeAvailable = true;
                    shopComponent.IsUpgradedEvent = true;
                }
                else
                {
                    shopComponent.IsUpgradedEvent = false;
                }
            }
        }
    }
}