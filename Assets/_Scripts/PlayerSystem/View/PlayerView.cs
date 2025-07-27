using System;
using UnityEngine;

namespace _Scripts.PlayerSystem.View
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private Collider2D col;

        public event Action OnJumpInput;
        public event Action<Collider2D> OnCollisionDetected;

        private void Awake()
        {
            if (rb == null) rb = GetComponent<Rigidbody2D>();
            if (col == null) col = GetComponent<Collider2D>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnJumpInput?.Invoke();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            OnCollisionDetected?.Invoke(other);
        }
        
        public void SetPosition(Vector3 position)
        { 
            transform.position = position;
        }
        
        public void ApplyJumpForce(float force)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, force);
        }
        
        public void SetGravity(float gravity)
        {
            rb.gravityScale = gravity / Physics2D.gravity.y;
        }
        
        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public bool IsGroundedCheck()
        {
            return rb.linearVelocity.y <= 0.1f && transform.position.y <= 0.1f;
        }
    }
}