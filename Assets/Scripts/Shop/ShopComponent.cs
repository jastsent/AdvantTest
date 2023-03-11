using AdvantTest.Configs;
using AdvantTest.Save;
using UnityEngine;
using UnityEngine.Localization;

namespace AdvantTest.Shop
{
    public struct ShopComponent
    {
        public LocalizedString Name => _config.Name;
        public float IncomeDelay => _config.IncomeDelay;
        public int BaseCost => _config.BaseCost; 
        public int BaseIncome => _config.BaseIncome;
        public LocalizedString FirstUpgradeName => _config.FirstUpgradeName;
        public int FirstUpgradePrice => _config.FirstUpgradePrice;
        public float FirstUpgradeMultiplier => _config.FirstUpgradeMultiplier;
        public LocalizedString SecondUpgradeName => _config.SecondUpgradeName;
        public int SecondUpgradePrice => _config.SecondUpgradePrice;
        public float SecondUpgradeMultiplier => _config.SecondUpgradeMultiplier;

        public int Level 
        {
            get => _shopData.Level;
            set
            {
                if (value == _shopData.Level) return;
                _shopData.Level = value;
                RecalculateIncome();
            }
        }

        public bool IsFirstUpgradeAvailable 
        {
            readonly get => _shopData.IsFirstUpgradeAvailable;
            set
            {
                if (value == _shopData.IsFirstUpgradeAvailable) return;
                _shopData.IsFirstUpgradeAvailable = value;
                RecalculateIncome();
            }
        }

        public bool IsSecondUpgradeAvailable 
        {
            readonly get => _shopData.IsSecondUpgradeAvailable;
            set
            {
                if (value == _shopData.IsSecondUpgradeAvailable) return;
                _shopData.IsSecondUpgradeAvailable = value; 
                RecalculateIncome();
            }
        }        
        
        public float IncomeProgress
        {
            get => _shopData.IncomeProgress;
            set => _shopData.IncomeProgress = value;
        }
        
        public int CurrentPrice => BaseCost * (Level + 1);
        public int CurrentIncome { get; private set; }
        public bool IsUpgradedEvent;

        private ShopConfig _config;
        private ShopData _shopData;

        public void SetConfig(ShopConfig config)
        {
            _config = config;
        }
        
        public void SetSaveData(ShopData shopData)
        {
            _shopData = shopData;
            RecalculateIncome();
        }
        
        private void RecalculateIncome()
        {
            CurrentIncome = Mathf.RoundToInt(Level * BaseIncome 
                * (FirstUpgradeMultiplier * (IsFirstUpgradeAvailable ? 1 : 0)
                   + SecondUpgradeMultiplier * (IsSecondUpgradeAvailable ? 1 : 0)
                   + 1));
        }
    }
}