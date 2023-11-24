using Scriptable;
using UnityEngine;

namespace Interactables
{
    public class Item : MonoBehaviour
    {
        private ShopItemScriptable _itemScriptable;

        public void SetItem(ShopItemScriptable value) => _itemScriptable = value;

        public ShopItemScriptable GetItem() => _itemScriptable;
    }
}
