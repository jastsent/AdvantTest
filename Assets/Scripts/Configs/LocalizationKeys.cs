using System;
using UnityEngine;
using UnityEngine.Localization;

namespace AdvantTest.Configs
{
    [Serializable]
    public sealed class LocalizationKeys
    {
        [field: SerializeField] public LocalizedString BalanceKey { get; private set; }
        [field: SerializeField] public LocalizedString LvlKey { get; private set; }
        [field: SerializeField] public LocalizedString IncomeKey { get; private set; }
        [field: SerializeField] public LocalizedString UpgradeIncomeKey { get; private set; }
        [field: SerializeField] public LocalizedString PriceKey { get; private set; }
        [field: SerializeField] public LocalizedString BoughtKey { get; private set; }
    }
}