using System;
using UnityEngine;
using UnityEngine.UI;
using CharacterController = Character.CharacterController;

namespace UI
{
    public class CharacterViewerUI : MonoBehaviour
    {
        [SerializeField] private Image headImage;
        [SerializeField] private Image bodyImage;

        private void Start()
        {
            CharacterController.Instance.OnInventoryUpdate += UpdateCharacterView;
            ClearCharacterView();
        }

        private void UpdateCharacterView()
        {
            ClearCharacterView();
           var items = CharacterController.Instance.GetItemsEquipped();
            foreach (var item in items)
            {
                switch (item.itemType)
                {
                    case ItemType.Head:
                        headImage.sprite = item.sprite;
                        headImage.color = new Color(255, 255, 255, 1);
                        break;
                    case ItemType.Body:
                        bodyImage.sprite = item.sprite;
                        bodyImage.color = new Color(255, 255, 255, 1);
                        break;
                    case ItemType.Foot:
                        break;
                    case ItemType.Hand:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void ClearCharacterView()
        {
            headImage.sprite = null;
            headImage.color = new Color(255, 255, 255, 0);
            bodyImage.sprite = null;
            bodyImage.color = new Color(255, 255, 255, 0);
        }
    }
}