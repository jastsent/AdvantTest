using AdvantTest.MainScreen;
using AdvantTest.Shop;
using UnityEngine;

namespace AdvantTest.Configs
{
    [CreateAssetMenu(menuName = "AdvantTest/Content", fileName = "Content")]
    public sealed class Content : ScriptableObject
    {
        [field: SerializeField] public MainScreenView MainScreenView { get; private set; }
        [field: SerializeField] public ShopView ShopView { get; private set; }
    }
}