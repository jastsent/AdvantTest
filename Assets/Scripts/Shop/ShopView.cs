using AdvantTest.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AdvantTest.Shop
{
    public sealed class ShopView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI incomeText;
        [SerializeField] private TextMeshProUGUI lvlText;
        [SerializeField] private TextMeshProUGUI priceText;
        [SerializeField] private Image progressBarMask;

        [SerializeField] private TextMeshProUGUI firstUpgradeNameText;
        [SerializeField] private TextMeshProUGUI firstUpgradeIncomeText;
        [SerializeField] private TextMeshProUGUI firstUpgradePriceText;
        
        [SerializeField] private TextMeshProUGUI secondUpgradeNameText;
        [SerializeField] private TextMeshProUGUI secondUpgradeIncomeText;
        [SerializeField] private TextMeshProUGUI secondUpgradePriceText;

        [field:SerializeField] public ButtonView LvlUpgradeButtonView { get; private set; }
        [field:SerializeField] public ButtonView FirstUpgradeButtonView { get; private set; }
        [field:SerializeField] public ButtonView SecondUpgradeButtonView { get; private set; }

        public void SetName(string shopName) => nameText.text = shopName;
        public void SetIncome(string income) => incomeText.text = income;
        public void SetLvl(string lvl) => lvlText.text = lvl;
        public void SetPrice(string price) => priceText.text = price;
        public void SetIncomeProgress(float value) => progressBarMask.fillAmount = value;
        
        public void SetFirstUpgradeInfo(string upgradeName, string upgradeIncome)
        {
            firstUpgradeNameText.text = upgradeName;
            firstUpgradeIncomeText.text = upgradeIncome;
        }
        public void SetFirstUpgradePrice(string price) => firstUpgradePriceText.text = price;
        
        public void SetSecondUpgradeInfo(string upgradeName, string upgradeIncome)
        {
            secondUpgradeNameText.text = upgradeName;
            secondUpgradeIncomeText.text = upgradeIncome;
        }
        public void SetSecondUpgradePrice(string price) => secondUpgradePriceText.text = price;
    }
}