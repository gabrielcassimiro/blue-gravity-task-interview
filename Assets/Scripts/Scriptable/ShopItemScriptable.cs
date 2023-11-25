using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(order = 0, menuName = "Item/Create new item", fileName = "New Item")]
    public class ShopItemScriptable : ScriptableObject
    {
        public string itemName;
        public Sprite icon;
        public Sprite sprite;
        public GameObject prefab;
        public int price;
        public int index;
        public ItemType itemType;
    }
}

public enum ItemType
{
    Head,
    Body,
    Foot,
    Hand
}