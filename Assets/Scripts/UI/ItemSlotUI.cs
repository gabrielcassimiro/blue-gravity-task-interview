using Scriptable;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using CharacterController = Character.CharacterController;

namespace UI
{
    public class ItemSlotUI : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private Image coinIcon;
        [SerializeField] private TextMeshProUGUI priceText;

        private ShopItemScriptable _item;
        private bool _inInventory;
        private bool _toSell;
        private bool _isEquipped;
        private ShopManager _shopManager;

        public void SetItem(ShopItemScriptable value, bool inventory = false, bool useSprite = false, bool equipped = false)
        {
            icon.sprite = useSprite ? value.sprite : value.icon;
            icon.preserveAspect = true;
            icon.color = new Color(255, 255, 255, 1);
            if (priceText && !_inInventory) priceText.text = $"{value.price}";
            if (coinIcon && !_inInventory) coinIcon.gameObject.SetActive(true);
            _item = value;
            _isEquipped = equipped;
            _inInventory = inventory;
        }

        public void SetItem(ShopItemScriptable value, ShopManager shop = null, bool toSell = false)
        {
            icon.sprite = value.icon;
            icon.preserveAspect = true;
            icon.color = new Color(255, 255, 255, 1);
            if (priceText) priceText.text = $"{value.price}";
            if (coinIcon) coinIcon.gameObject.SetActive(true);
            _item = value;
            _shopManager = shop;
            _toSell = toSell;
        }

        public void ClearItem()
        {
            icon.sprite = null;
            icon.color = new Color(255, 255, 255, 0);
            if (priceText) priceText.text = string.Empty;
            if (coinIcon) coinIcon.gameObject.SetActive(false);
            _item = null;
        }

        public void EquipItem()
        {
            if (!_inInventory || !_item || _isEquipped) return;
            CharacterController.Instance.EquipItemClicked(_item);
            
        }

        public void UnEquipItem()
        {
            if (!_inInventory || !_item || !_isEquipped) return;
            CharacterController.Instance.UnEquipItem(_item);
            
        }
        
        public void BuyItem()
        {
            if (!CharacterController.Instance || !_shopManager || _toSell) return;
            if (CharacterController.Instance.GetMoney() < _item.price) return;
            CharacterController.Instance.ChangeMoney(-_item.price);
            CharacterController.Instance.AddItem(_item);
            _shopManager.RemoveItem(_item);
        }

        public void SellItem()
        {
            if (!CharacterController.Instance || !_shopManager || !_toSell) return;
            _shopManager.AddItem(_item);
            CharacterController.Instance.ChangeMoney(_item.price);
            CharacterController.Instance.SellItem(_item);
        }
    }
}