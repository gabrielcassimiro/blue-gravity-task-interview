using System.Collections.Generic;
using System.Linq;
using Interactables;
using Scriptable;
using UI;
using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(CharacterMovement), typeof(CharacterInteraction))]
    public class CharacterController : MonoBehaviour
    {
        public static CharacterController Instance;

        public delegate void InventoryUpdate();

        public InventoryUpdate OnInventoryUpdate;

        [Header("Character Properties")] [SerializeField]
        private int money;

        [Header("Items Properties")] [SerializeField]
        private List<ShopItemScriptable> items;

        [SerializeField] private List<ShopItemScriptable> itemsEquipped;

        private PlayerInputActions _input;
        private List<Item> _itemsEquipped;

        private void Awake()
        {
            if (Instance) Destroy(gameObject);
            else Instance = this;

            _input = new PlayerInputActions();

            items ??= new List<ShopItemScriptable>();
            itemsEquipped ??= new List<ShopItemScriptable>();
            _itemsEquipped = new List<Item>();
        }

        private void Start()
        {
            foreach (var item in itemsEquipped)
            {
                EquipItem(item);
            }

            GameplayUI.Instance.UpdateMoneyText(money);
        }

        #region Methods: Public

        public PlayerInputActions.GameplayActions GetActions() => _input.Gameplay;

        public List<ShopItemScriptable> GetItemsEquipped() => itemsEquipped;
        public List<ShopItemScriptable> GetItems() => items;

        public void EquipItemClicked(ShopItemScriptable item)
        {
            var sameItem = itemsEquipped.FirstOrDefault(x => x == item);
            if (sameItem) return;
            var itemToRemove = itemsEquipped.FirstOrDefault(x => x.itemType == item.itemType);
            if (itemToRemove != null)
            {
                items.Add(itemToRemove);
                items.Remove(item);
                itemsEquipped.Remove(itemToRemove);
                itemsEquipped.Add(item);
                RemoveItem(itemToRemove);
                EquipItem(item);
            }
            else
            {
                items.Remove(item);
                itemsEquipped.Add(item);
                EquipItem(item);
            }

            if (InventoryManagerUI.Instance)
            {
                InventoryManagerUI.Instance.UpdateInventory();
                OnInventoryUpdate?.Invoke();
            }
        }

        public void UnEquipItem(ShopItemScriptable item)
        {
            var desiredItem = itemsEquipped.FirstOrDefault(x => x == item);
            items.Add(desiredItem);
            itemsEquipped.Remove(desiredItem);
            
            if (InventoryManagerUI.Instance)
            {
                InventoryManagerUI.Instance.UpdateInventory();
            }
            OnInventoryUpdate?.Invoke();
        }

        public int GetMoney() => money;

        public void ChangeMoney(int value)
        {
            money += value;
            GameplayUI.Instance.UpdateMoneyText(money);
        }

        public void AddItem(ShopItemScriptable item)
        {
            items.Add(item);

            if (InventoryManagerUI.Instance && InventoryManagerUI.Instance.gameObject.activeSelf)
            {
                InventoryManagerUI.Instance.UpdateInventory();
                OnInventoryUpdate?.Invoke();
            }
        }

        public void SellItem(ShopItemScriptable item)
        {
            items.Remove(item);

            if (InventoryManagerUI.Instance && InventoryManagerUI.Instance.gameObject.activeSelf)
            {
                InventoryManagerUI.Instance.UpdateInventory();
            }
            OnInventoryUpdate?.Invoke();
        }

        #endregion

        #region Methods: Private

        private void OnEnable()
        {
            _input.Enable();
        }

        private void OnDisable()
        {
            _input.Disable();
        }

        private void EquipItem(ShopItemScriptable item)
        {
            var go = Instantiate(item.prefab, gameObject.transform);
            var i = go.GetComponent<Item>();
            i.SetItem(item);
            _itemsEquipped.Add(i);
        }

        private void RemoveItem(ShopItemScriptable item)
        {
            var itemToRemove = _itemsEquipped.FirstOrDefault(x => x.GetItem() == item);
            if (!itemToRemove) return;
            _itemsEquipped.Remove(itemToRemove);
            Destroy(itemToRemove.gameObject);
        }

        #endregion
    }
}