using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SoundManagerUI : MonoBehaviour
    {
        [Header("Sprite Properties")] [SerializeField]
        private Sprite soundOn;
        [SerializeField] private Sprite soundOff;
        
        [Header("Sound Properties")]
        [SerializeField] private AudioSource sound;

        private bool _playing = true;

        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(PlaySound);
        }

        private void PlaySound()
        {
            if (_playing) sound.Stop();
            else sound.Play();
            _playing = !_playing;
            GetComponent<Image>().sprite = _playing ? soundOn : soundOff;

        }
    }
}