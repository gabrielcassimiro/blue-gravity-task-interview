using System;
using System.Collections.Generic;
using System.Linq;
using Scriptable;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using CharacterController = Character.CharacterController;

namespace UI
{
    public class ShopManager : MonoBehaviour
    {
        [Header("Panel Properties")] [SerializeField]
        private TextMeshProUGUI subTitleText;

        [SerializeField] private Button buySectionButton;
        [SerializeField] private Button sellSectionButton;

        [Header("Slots and Items Properties")] [SerializeField]
        private List<ShopItemScriptable> items;

        [SerializeField] private List<ItemSlotUI> slots;

        private bool _showSellItems;

        private void Start()
        {
            buySectionButton.onClick.AddListener(ShowItems);
            sellSectionButton.onClick.AddListener(ShowSellItems);
            CharacterController.Instance.OnInventoryUpdate += UpdateInventory;
        }

        #region Methods: Public

        public void ShowItems()
        {
            subTitleText.text = "Buy";
            _showSellItems = false;

            if (items == null || slots == null) return;
            ClearItems();
            var itemsOrdered = items.OrderBy(x => x.index).ToList();
            for (int i = 0; i < itemsOrdered.Count; i++)
            {
                slots[i].SetItem(itemsOrdered[i], shop: this);
            }
        }

        public void AddItem(ShopItemScriptable item)
        {
            items.Add(item);
            UpdateInventory();
        }

        public void RemoveItem(ShopItemScriptable item)
        {
            items.Remove(item);
            ShowItems();
        }

        #endregion

        #region Methods: Private

        private void UpdateInventory()
        {
            if (_showSellItems)
                ShowSellItems();
            else
                ShowItems();
        }

        private void ClearItems()
        {
            foreach (var slot in slots)
            {
                slot.ClearItem();
            }
        }

        private void ShowSellItems()
        {
            subTitleText.text = "Sell";
            _showSellItems = true;

            var sellItems = CharacterController.Instance.GetItems();

            if (sellItems == null || slots == null) return;
            ClearItems();
            var itemsOrdered = sellItems.OrderBy(x => x.index).ToList();
            for (int i = 0; i < itemsOrdered.Count; i++)
            {
                slots[i].SetItem(itemsOrdered[i], shop: this, toSell: true);
            }
        }

        #endregion
    }
}