using AdvantTest.Base;
using AdvantTest.Configs;
using AdvantTest.Utils;
using Leopotam.EcsLite;
using UnityEngine;

namespace AdvantTest.MainScreen
{
    public sealed class MainScreenViewSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly Content _content;
        private readonly Canvas _canvas;
        private EcsFilter _balanceFilter;
        private EcsPool<BalanceComponent> _balancePool;
        private EcsFilter _mainScreenViewFilter;
        private EcsPool<MainScreenViewComponent> _mainScreenViewPool;

        public MainScreenViewSystem(Content content, Canvas canvas)
        {
            _content = content;
            _canvas = canvas;
        }
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            
            _balanceFilter = world.Filter<BalanceComponent>().End();
            _balancePool = world.GetPool<BalanceComponent>();
            _mainScreenViewFilter = world.Filter<MainScreenViewComponent>().End();
            _mainScreenViewPool = world.GetPool<MainScreenViewComponent>();
            
            var newEntity = world.NewEntity();
            ref var mainScreenViewComponent = ref _mainScreenViewPool.Add(newEntity);
            mainScreenViewComponent.MainScreenView = Object.Instantiate(_content.MainScreenView, _canvas.transform);
        }

        public void Run(IEcsSystems systems)
        {
            var entity = _balanceFilter.GetFirstEntity();
            ref var balanceComponent = ref _balancePool.Get(entity);

            entity = _mainScreenViewFilter.GetFirstEntity();
            ref var mainScreenView = ref _mainScreenViewPool.Get(entity).MainScreenView;
            
            mainScreenView.SeBalance(balanceComponent.Balance.ToString());
        }
    }
}