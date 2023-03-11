using System.Collections.Generic;
using UnityEngine;

namespace AdvantTest.Configs
{
    [CreateAssetMenu(menuName = "AdvantTest/Config", fileName = "Config")]
    public sealed class Config : ScriptableObject
    {
        [SerializeField] private List<ShopConfig> shopConfigs;
        [field: SerializeField] public LocalizationKeys LocalizationKeys { get; private set; }

        public IReadOnlyList<ShopConfig> ShopConfigs => shopConfigs;
    }
}