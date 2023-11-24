using System;
using NPCs;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Character
{
    public class CharacterInteraction : MonoBehaviour
    {
        [SerializeField] private InventoryManagerUI inventory;
        private GameObject _gameObjectTriggered;

        private void Start()
        {
            CharacterController.Instance.GetActions().Interact.performed += TryInteract;
            CharacterController.Instance.GetActions().Inventory.performed += ShowInventory;
            CharacterController.Instance.GetActions().Cancel.performed += CloseShop;
        }

        private void CloseShop(InputAction.CallbackContext ctx)
        {
            var pressed = ctx.ReadValueAsButton();
            if (pressed)
            {
                if (!inventory) return;
                inventory.gameObject.SetActive(false);
            }
        }

        private void TryInteract(InputAction.CallbackContext ctx)
        {
            var pressed = ctx.ReadValueAsButton();
            if (pressed)
            {
                if (!_gameObjectTriggered) return;
                var shopKeeper = _gameObjectTriggered.GetComponent<ShopKeeper>();
                if (shopKeeper)
                {
                    shopKeeper.ShowShop(true);
                }
            }
        }
        
        private void ShowInventory(InputAction.CallbackContext ctx)
        {
            var pressed = ctx.ReadValueAsButton();
            if (pressed)
            {
                if (!inventory) return;
                inventory.gameObject.SetActive(!inventory.gameObject.activeSelf);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("NPC_Interactable"))
            {

                if (GameplayUI.Instance)
                {
                    GameplayUI.Instance.ShowInteractTextPanel(true);
                    _gameObjectTriggered = other.gameObject;
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("NPC_Interactable"))
            {

                if (GameplayUI.Instance)
                {
                    GameplayUI.Instance.ShowInteractTextPanel(false);
                    _gameObjectTriggered = null;
                }
            }
        }
    }
}