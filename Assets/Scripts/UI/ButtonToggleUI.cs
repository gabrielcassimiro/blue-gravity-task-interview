using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ButtonToggleUI : MonoBehaviour
    {
        [SerializeField] private GameObject desiredObject;

        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(ShowObject);
        }

        private void ShowObject()
        {
            desiredObject.SetActive(!desiredObject.activeSelf);
        }
    }
}
