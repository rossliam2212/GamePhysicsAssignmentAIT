using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Players {
    public class Character : MonoBehaviour {

        // Components/Game Objects
        protected Rigidbody2D Rb;
        protected Animator Animator;
        
        // Bool Flags
        [SerializeField] protected bool isDead;
        protected bool IsFacingRight;
        protected bool IsGrounded;
        
        // Variables
        private const int MaxHealth = 100;
        [SerializeField] private int currentHealth;

        protected float MoveSpeed;
        protected float JumpForce;

        protected const int Left = -1;
        protected const int Right = 1;
        protected const int Idle = 0;

        protected void Start() {
            Rb = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();

            isDead = false;
            IsFacingRight = true;
            IsGrounded = true;
            currentHealth = MaxHealth;
        }

        private void Update() { }

        protected void Move(float move) {
            var direction = Idle;
            
            if (move > 0) direction = Right;
            else if (move < 0) direction = Left;

            Flip(direction);

            var xValue = move * 100 * Time.deltaTime * MoveSpeed;
            Rb.velocity = new Vector2(xValue, Rb.velocity.y);
        }

        protected void Jump() {
            Rb.AddForce(new Vector2(0f, JumpForce));
            IsGrounded = false;
        }

        protected void Flip(int direction) {
            if (IsFacingRight && direction < 0 || !IsFacingRight && direction > 0) {
                IsFacingRight = !IsFacingRight;
                transform.Rotate(0f, 180f, 0f);
            }
        }

        protected void TakeDamage(int damage) {
            currentHealth -= damage;
            if (currentHealth <= 0)
                Kill();
        }

        private void Kill() {
            isDead = true;
            Invoke(nameof(DestroyObject), 0f);
        }

        private void DestroyObject() {
            Destroy(gameObject);
        }

        public void SetIsGrounded(bool isGrounded) { IsGrounded = isGrounded; }
        public bool GetIsFacingRight() { return IsFacingRight; }
    }
}