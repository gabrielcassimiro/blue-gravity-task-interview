using System.Collections.Generic;
using System.Linq;
using Scriptable;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ShopManager : MonoBehaviour
    {
        [SerializeField] private List<ShopItemScriptable> items;
        [SerializeField] private List<ItemSlotUI> slots;

        public void ShowItems()
        {
            if (items == null || slots == null) return;
            ClearItems();
            var itemsOrdered = items.OrderBy(x => x.index).ToList();
            for (int i = 0; i < itemsOrdered.Count; i++)
            {
                slots[i].SetItem(itemsOrdered[i]);
            }
        }

        private void ClearItems()
        {
            foreach (var slot in slots)
            {
                slot.ClearItem();
            }
        }
    }
}