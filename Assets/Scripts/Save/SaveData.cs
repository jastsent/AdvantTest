using System;

namespace AdvantTest.Save
{
    [Serializable]
    public sealed class SaveData
    {
        public int Balance;
        public ShopData[] ShopsData;
    }

    [Serializable]
    public sealed class ShopData
    {
        public int Level;
        public bool IsFirstUpgradeAvailable;
        public bool IsSecondUpgradeAvailable;
    }
}