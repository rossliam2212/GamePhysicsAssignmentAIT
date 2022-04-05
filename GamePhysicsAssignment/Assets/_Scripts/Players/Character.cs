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

        // Initialization
        protected void Start() {
            Rb = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();

            isDead = false;
            IsFacingRight = true;
            IsGrounded = true;
            currentHealth = MaxHealth;
        }

        /// <summary>
        /// Moves the Character along the x-axis.
        /// </summary>
        /// <param name="move">The direction along the x-axis.</param>
        protected void Move(float move) {
            CurrentDirection = Idle;
            
            if (move > 0) CurrentDirection = Right;
            else if (move < 0) CurrentDirection = Left;

            Flip(CurrentDirection);

            var xValue = move * 100 * Time.deltaTime * MoveSpeed;
            Rb.velocity = new Vector2(xValue, Rb.velocity.y);
        }

        /// <summary>
        /// Default Character Jump method. Calls the overloaded Jump(float) method with the default JumpForce.
        /// </summary>
        protected void Jump() {
            Jump(JumpForce);
        }

        /// <summary>
        /// Overloaded Character Jump method. Adds a specific jumpForce to the Characters rb.
        /// </summary>
        /// <param name="jumpForce"></param>
        protected void Jump(float jumpForce) {
            Rb.AddForce(new Vector2(0f, jumpForce));
            IsGrounded = false;
        }
        

        /// <summary>
        /// Flips the Character to face the correct direction they are moving in.
        /// </summary>
        /// <param name="direction">The Characters current direction.</param>
        protected void Flip(int direction) {
            if (IsFacingRight && direction < 0 || !IsFacingRight && direction > 0) {
                IsFacingRight = !IsFacingRight;
                transform.Rotate(0f, 180f, 0f);
            }
        }

        /// <summary>
        /// Deals damage to the Character when they get hit.
        /// </summary>
        /// <param name="damage">The amount of damage to deal.</param>
        public virtual void TakeDamage(int damage) {
            currentHealth -= damage;
            if (currentHealth <= 0)
                Kill();
        }

        /// <summary>
        /// Kills the Character when they have no health left.
        /// </summary>
        protected void Kill() {
            isDead = true;
            Invoke(nameof(DestroyObject), 0.5f);
        }

        private void DestroyObject() {
            Destroy(gameObject);
        }

        /// <summary>
        /// Setter method from IsGrounded.
        /// </summary>
        /// <param name="isGrounded">Value to set isGrounded to.</param>
        public void SetIsGrounded(bool isGrounded) { IsGrounded = isGrounded; }

        /// <summary>
        /// Getter method for IsFacingRight.
        /// </summary>
        /// <returns>True if the player is facing right, False otherwise.</returns>
        public bool GetIsFacingRight() { return IsFacingRight; }
    }
}