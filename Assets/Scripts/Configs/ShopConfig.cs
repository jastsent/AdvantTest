using System;
using UnityEngine;
using UnityEngine.Localization;

namespace AdvantTest.Configs
{
    [Serializable]
    public sealed class ShopConfig
    {
        [field: SerializeField] public LocalizedString Name { get; private set; }
        [field: SerializeField] public float IncomeDelay { get; private set; }
        [field: SerializeField] public int BaseCost { get; private set; }
        [field: SerializeField] public int BaseIncome { get; private set; }
        
        [field: SerializeField] public LocalizedString FirstUpgradeName { get; private set; }
        [field: SerializeField] public int FirstUpgradePrice { get; private set; }
        [field: SerializeField] public float FirstUpgradeMultiplier { get; private set; }
        
        [field: SerializeField] public LocalizedString SecondUpgradeName { get; private set; }
        [field: SerializeField] public int SecondUpgradePrice { get; private set; }
        [field: SerializeField] public float SecondUpgradeMultiplier { get; private set; }
    }
}