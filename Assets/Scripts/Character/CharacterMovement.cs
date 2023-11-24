using System;
using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
    public class CharacterMovement : MonoBehaviour
    {
        
        public static CharacterMovement Instance;
        
        [SerializeField] private float speed = 5f;
    
        private Rigidbody2D _rb;

        #region Delegates

        public delegate void AnimationUpdate(Vector2 movement, float speed);

        public AnimationUpdate OnAnimationUpdate;

        private Vector2 _movement;
        
        #endregion

        private void Awake()
        {
            if (Instance) Destroy(gameObject);
            else Instance = this;
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            Move();
        }

        #region Methods: Private

        private void Move()
        {
            if (!_rb) return;
            _movement = CharacterController.Instance.GetActions().Movement.ReadValue<Vector2>();
            _rb.MovePosition(_rb.position + _movement * speed * Time.fixedDeltaTime);
            OnAnimationUpdate?.Invoke(_movement, _movement.sqrMagnitude);
        }

        #endregion
    }
}
