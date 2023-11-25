using UnityEngine;

namespace NPCs
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Npc : MonoBehaviour
    {
        [SerializeField] private GameObject boxInteraction;
        private bool _show;
        
        public void ShowInteraction()
        {
            if(_show) return;
            _show = true;
            boxInteraction.gameObject.SetActive(true);
            Invoke(nameof(HideInteraction), 3f);
        }
        
        private void HideInteraction()
        {
            boxInteraction.gameObject.SetActive(false);
            _show = false;
        }
    }
}
