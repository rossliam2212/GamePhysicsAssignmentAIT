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
        [SerializeField] protected int currentHealth;

        protected float MoveSpeed;
        protected float JumpForce;

        protected int CurrentDirection;
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
            CurrentDirection = Idle;
            
            if (move > 0) CurrentDirection = Right;
            else if (move < 0) CurrentDirection = Left;

            Flip(CurrentDirection);

            var xValue = move * 100 * Time.deltaTime * MoveSpeed;
            Rb.velocity = new Vector2(xValue, Rb.velocity.y);
        }

        protected void Jump() {
            Jump(JumpForce);
        }

        protected void Jump(float jumpForce) {
            Rb.AddForce(new Vector2(0f, jumpForce));
            IsGrounded = false;
        }

        protected void Flip(int direction) {
            if (IsFacingRight && direction < 0 || !IsFacingRight && direction > 0) {
                IsFacingRight = !IsFacingRight;
                transform.Rotate(0f, 180f, 0f);
            }
        }

        public virtual void TakeDamage(int damage) {
            currentHealth -= damage;
            if (currentHealth <= 0)
                Kill();
        }

        private void Kill() {
            isDead = true;
            Invoke(nameof(DestroyObject), 0.5f);
        }

        private void DestroyObject() {
            Destroy(gameObject);
        }

        public void SetIsGrounded(bool isGrounded) { IsGrounded = isGrounded; }
        public bool GetIsFacingRight() { return IsFacingRight; }
    }
}