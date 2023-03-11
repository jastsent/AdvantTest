using AdvantTest.Configs;
using AdvantTest.MainScreen;
using AdvantTest.Utils;
using Leopotam.EcsLite;
using UnityEngine;

namespace AdvantTest.Shop
{
    public sealed class ShopViewSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly Config _config;
        private readonly Content _content;
        private EcsFilter _shopFilter;
        private EcsPool<ShopComponent> _shopPool;
        private EcsPool<ShopViewComponent> _shopViewPool;

        public ShopViewSystem(Config config, Content content)
        {
            _config = config;
            _content = content;
        }

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            _shopFilter = world.Filter<ShopComponent>().Inc<ShopViewComponent>().End();
            _shopPool = world.GetPool<ShopComponent>();
            _shopViewPool = world.GetPool<ShopViewComponent>();
            
            var mainScreenViewFilter = world.Filter<MainScreenViewComponent>().End();
            var mainScreenViewPool = world.GetPool<MainScreenViewComponent>();
            ref var mainScreenViewComponent = ref mainScreenViewPool.Get(mainScreenViewFilter.GetFirstEntity());
            var shopFilter = world.Filter<ShopComponent>().Exc<ShopViewComponent>().End();
            foreach (var entity in shopFilter)
            {
                ref var shopComponent = ref _shopPool.Get(entity);

                var shopView = Object.Instantiate(_content.ShopView, mainScreenViewComponent.MainScreenView.ContentTransform);
                shopView.SetName(shopComponent.Name.GetLocalizedString());
                shopView.SetFirstUpgradeInfo(shopComponent.FirstUpgradeName.GetLocalizedString(),
                    _config.LocalizationKeys.UpgradeIncomeKey.GetLocalizedString(shopComponent.FirstUpgradeMultiplier * 100));
                shopView.SetSecondUpgradeInfo(shopComponent.SecondUpgradeName.GetLocalizedString(),
                    _config.LocalizationKeys.UpgradeIncomeKey.GetLocalizedString(shopComponent.SecondUpgradeMultiplier * 100));
                shopView.SetLvl(_config.LocalizationKeys.LvlKey.GetLocalizedString(shopComponent.Level));
                shopView.SetIncome(_config.LocalizationKeys.IncomeKey.GetLocalizedString(shopComponent.CurrentIncome));
                shopView.SetPrice(_config.LocalizationKeys.PriceKey.GetLocalizedString(shopComponent.CurrentPrice));
                shopView.SetFirstUpgradePrice(shopComponent.IsFirstUpgradeAvailable
                    ? _config.LocalizationKeys.BoughtKey.GetLocalizedString()
                    : _config.LocalizationKeys.PriceKey.GetLocalizedString(shopComponent.FirstUpgradePrice));
                shopView.SetSecondUpgradePrice(shopComponent.IsSecondUpgradeAvailable
                    ? _config.LocalizationKeys.BoughtKey.GetLocalizedString()
                    : _config.LocalizationKeys.PriceKey.GetLocalizedString(shopComponent.SecondUpgradePrice));

                ref var shopViewComponent = ref _shopViewPool.Add(entity);
                shopViewComponent.ShopView = shopView;
            }
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _shopFilter)
            {
                ref var shopComponent = ref _shopPool.Get(entity);
                ref var shopView = ref _shopViewPool.Get(entity).ShopView;
                
                shopView.SetIncomeProgress(Mathf.Clamp01(shopComponent.IncomeTimer / shopComponent.IncomeDelay));

                if (shopComponent.IsUpgradedEvent)
                {
                    shopView.SetLvl(_config.LocalizationKeys.LvlKey.GetLocalizedString(shopComponent.Level));
                    shopView.SetIncome(_config.LocalizationKeys.IncomeKey.GetLocalizedString(shopComponent.CurrentIncome));
                    shopView.SetPrice(_config.LocalizationKeys.PriceKey.GetLocalizedString(shopComponent.CurrentPrice));
                    shopView.SetFirstUpgradePrice(shopComponent.IsFirstUpgradeAvailable
                        ? _config.LocalizationKeys.BoughtKey.GetLocalizedString()
                        : _config.LocalizationKeys.PriceKey.GetLocalizedString(shopComponent.FirstUpgradePrice));
                    shopView.SetSecondUpgradePrice(shopComponent.IsSecondUpgradeAvailable
                        ? _config.LocalizationKeys.BoughtKey.GetLocalizedString()
                        : _config.LocalizationKeys.PriceKey.GetLocalizedString(shopComponent.SecondUpgradePrice));
                }
            }
        }
    }
}