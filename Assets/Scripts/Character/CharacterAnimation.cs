using System;
using UnityEngine;

namespace Character
{
    public class CharacterAnimation : MonoBehaviour
    {
        [SerializeField] private AudioSource sound;
        
        private Animator _animator;
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int Vertical = Animator.StringToHash("Vertical");
        private static readonly int Horizontal = Animator.StringToHash("Horizontal");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            if (CharacterMovement.Instance)
                CharacterMovement.Instance.OnAnimationUpdate += AnimationUpdate;
        }

        public void PlaySound()
        {
            if(!sound) return;
            sound.Play();
        }

        private void AnimationUpdate(Vector2 movement, float speed)
        {
            if(!_animator) return;
            _animator.SetFloat(Horizontal, movement.x);
            _animator.SetFloat(Vertical, movement.y);
            _animator.SetFloat(Speed, speed);
        }
    }
}
