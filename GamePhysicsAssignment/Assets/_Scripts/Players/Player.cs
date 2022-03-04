using System;
using System.Collections;
using System.Collections.Generic;
using Objects;
using UnityEngine;

namespace Players {
    public class Player : Character {
        
        // Components/Game Objects
        private PlayerAnimationManager _animationManager;
        [SerializeField] private Transform hitPoint;
        [SerializeField] private LayerMask interactablesLayer;
        
        // Bool Flags
        private bool isIdle = true;
        private bool isRunning = false;
        
        private bool isJumpPressed = false;
        private bool isJumping = false;
        
        private bool isHitPressed = false;
        private bool isHitting = false;

        private bool hitCoolDownTimer = false;
        
        // Variables
        [SerializeField] private float moveSpeed = 3f;
        [SerializeField] private float jumpForce = 400f;
        [SerializeField] private float dashAmount = 4f;
        [SerializeField] private float hitRange = 3f;
        private float _directionX;
        private float hitCoolDown = 0.5f;

        private new void Start() {
            base.Start();
            _animationManager = GetComponent<PlayerAnimationManager>();
            MoveSpeed = moveSpeed;
            JumpForce = jumpForce;
        }

        private void Update() {
            GetInput();

            if (hitCoolDownTimer) {
                hitCoolDown -= Time.deltaTime;
                if (hitCoolDown <= 0f) {
                    hitCoolDown = 0.5f;
                    hitCoolDownTimer = false;
                    isHitting = false;
                }
            }
        }

        private void FixedUpdate() {
            if (!isDead) {
                Move(_directionX);
                
                if (IsGrounded && !isHitting) 
                    _animationManager.ChangeAnimationState(_directionX != 0 ? State.PlayerRunning : State.PlayerIdle);

                if (isJumpPressed && IsGrounded && !isHitting) {
                    isJumpPressed = false;
                    if (!isJumping) {
                        isJumping = true;
                        _animationManager.ChangeAnimationState(State.PlayerJumping);
                        Jump();
                    }
                }

                if (isHitPressed && IsGrounded && !isJumping) {
                    isHitPressed = false;
                    if (!isHitting) {
                        isHitting = true;
                        _animationManager.ChangeAnimationState(State.PlayerHitting);
                        CheckHit();
                        hitCoolDownTimer = true;
                    }
                }
            }
        }

        private void GetInput() {
            _directionX = Input.GetAxis("Horizontal");

            if (Input.GetButtonDown("Jump"))
                isJumpPressed = true;

            if (Input.GetMouseButton(0))
                isHitPressed = true;
        }

        private void CheckHit() {
            var hit = Physics2D.OverlapCircleAll(hitPoint.position, hitRange, interactablesLayer);
            foreach (var obj in hit) {
                if (obj.GetComponent<TurnableSlant>()) {
                    obj.GetComponent<TurnableSlant>().Hit();
                }

                if (obj.GetComponent<Switch>()) {
                    obj.GetComponent<Switch>().FlipSwitch();
                }
            }
        }
        
        private void OnDrawGizmosSelected() {
            if (hitPoint == null) return;
            Gizmos.DrawWireSphere(hitPoint.position, hitRange);
        }
        
        public bool IsJumping {
            get => isJumping;
            set => isJumping = value;
        }
    }
}