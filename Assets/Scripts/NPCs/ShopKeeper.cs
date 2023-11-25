using System;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;
using CharacterController = Character.CharacterController;

namespace NPCs
{
    [RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
    public class ShopKeeper : MonoBehaviour
    {
        [SerializeField] private ShopManager shopPanel;
        
        private Rigidbody2D _rb;
        private BoxCollider2D _collider;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _rb.gravityScale = 0f;
            _collider = GetComponent<BoxCollider2D>();
            _collider.isTrigger = true;
        }

        private void Start()
        {
            CharacterController.Instance.GetActions().Cancel.performed += CloseShop;
            if (shopPanel && shopPanel.gameObject.activeSelf) ShowShop(false);
        }
        
        

        #region Methods: Public
        
        public void ShowShop(bool value)
        {
            if (shopPanel)
            {
                shopPanel.gameObject.SetActive(value);
                shopPanel.ShowItems();
            }
        }
        
        public void CloseShop()
        {
            if(shopPanel && shopPanel.gameObject.activeSelf)
                ShowShop(false);
        }

        #endregion

        #region Methods: Private

        private void CloseShop(InputAction.CallbackContext ctx)
        {
            var pressed = ctx.ReadValueAsButton();
            if (pressed)
            {
                if(shopPanel && shopPanel.gameObject.activeSelf)
                    ShowShop(false);
            }
        }

        #endregion
    }
}
