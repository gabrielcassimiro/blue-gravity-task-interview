using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class GameplayUI : MonoBehaviour
    {
        public static GameplayUI Instance;

        private void Awake()
        {
            if (Instance) Destroy(Instance);
            else Instance = this;
        }

        [Header("Character Properties")] [SerializeField]
        private TextMeshProUGUI moneyText;
        
        [Header("Interact Text Properties")] [SerializeField]
        private GameObject interactTextPanel;

        #region Methods: Public

        public void ShowInteractTextPanel(bool value)
        {
            if(!interactTextPanel) return;
            interactTextPanel.SetActive(value);
        }

        public void UpdateMoneyText(int value)
        {
            if (!moneyText) return;
            moneyText.text = $"{value}";
        }

        #endregion
    }
}
