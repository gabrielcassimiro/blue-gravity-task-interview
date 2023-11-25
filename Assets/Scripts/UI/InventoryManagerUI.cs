using System;
using System.Collections.Generic;
using Scriptable;
using UnityEngine;
using CharacterController = Character.CharacterController;

namespace UI
{
    public class InventoryManagerUI : MonoBehaviour
    {
        [SerializeField] private List<ShopItemScriptable> items;
        [SerializeField] private List<ShopItemScriptable> itemsEquipped;
        [SerializeField] private List<ItemSlotUI> slots;
        [SerializeField] private ItemSlotUI headSlot;
        [SerializeField] private ItemSlotUI bodySlot;
        [SerializeField] private ItemSlotUI footSlot;
        [SerializeField] private ItemSlotUI handSlot;
        [SerializeField] private ItemSlotUI headView;
        [SerializeField] private ItemSlotUI bodyView;
        [SerializeField] private ItemSlotUI footView;
        [SerializeField] private ItemSlotUI handView;
        
        public static InventoryManagerUI Instance;

        private void Awake()
        {
            if (Instance) Destroy(gameObject);
            else Instance = this;
        }

        private void OnEnable()
        {
            UpdateInventory();
        }

        public void UpdateInventory()
        {
            itemsEquipped = new List<ShopItemScriptable>();
            itemsEquipped = CharacterController.Instance.GetItemsEquipped();
            items = new List<ShopItemScriptable>();
            items = CharacterController.Instance.GetItems();
            ShowItems();
            ShowEquippedItems();
        }

        public void ShowEquippedItems()
        {
            ClearEquippedItems();
            if (itemsEquipped == null || slots == null) return;
            foreach (var item in itemsEquipped)
            {
                if(headSlot && item.itemType == ItemType.Head) headSlot.SetItem(item, true, equipped: true);
                if(bodySlot && item.itemType == ItemType.Body) bodySlot.SetItem(item, true, equipped: true);
                if(footSlot && item.itemType == ItemType.Foot) footSlot.SetItem(item, true, equipped: true);
                if(handSlot && item.itemType == ItemType.Hand) handSlot.SetItem(item, true, equipped: true);
                
                if(headView && item.itemType == ItemType.Head) headView.SetItem(item, useSprite: true);
                if(bodyView && item.itemType == ItemType.Body) bodyView.SetItem(item, useSprite: true);
                if(footView && item.itemType == ItemType.Foot) footView.SetItem(item, useSprite: true);
                if(handView && item.itemType == ItemType.Hand) handView.SetItem(item, useSprite: true);
            }
        }

        public void ShowItems()
        {
            if (items == null || slots == null) return;
            ClearItems();
            for (int i = 0; i < items.Count; i++)
            {
                slots[i].SetItem(items[i], true);
            }
        }

        private void ClearItems()
        {
            foreach (var slot in slots)
            {
                slot.ClearItem();
            }
        }
        
        private void ClearEquippedItems()
        {
            if(headSlot) headSlot.ClearItem();
            if(bodySlot) bodySlot.ClearItem();
            if(footSlot) footSlot.ClearItem();
            if(handSlot) handSlot.ClearItem();
            if(headView) headView.ClearItem();
            if(bodyView) bodyView.ClearItem();
            if(footView) footView.ClearItem();
            if(handView) handView.ClearItem();
        }
    }
}
