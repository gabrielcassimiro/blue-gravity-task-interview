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


        public void SetItem(ShopItemScriptable value, bool inventory = false)
        {
            icon.sprite = value.icon;
            icon.preserveAspect = true;
            icon.color = new Color(255, 255, 255, 1);
            if (priceText) priceText.text = $"{value.price}";
            if (coinIcon) coinIcon.gameObject.SetActive(true);
            _item = value;
            _inInventory = inventory;
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
            if (!_inInventory || !_item) return;
            CharacterController.Instance.EquipItemClicked(_item);
        }
    }
}