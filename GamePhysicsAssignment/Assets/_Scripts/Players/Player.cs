using System;
using System.Collections;
using Objects;
using Unity.Mathematics;
using UnityEngine;

namespace Players {
    public class Player : Character {
        
        // Components/Game Objects
        private PlayerAnimationManager _animationManager;
        [SerializeField] private Transform hitPoint;
        [SerializeField] private LayerMask interactablesLayer;
        
        [SerializeField] private GameObject ball;
        private GameObject _currentBall;

        [Space]

        // Bool Flags
        private bool isIdle = true;
        private bool isRunning = false;
        
        private bool isFlattened = false;

        private bool isJumpPressed = false;
        private bool isJumping = false;
        
        private bool isHitPressed = false;
        private bool isHitting = false;

        private bool hitCoolDownTimer = false;

        private bool canSpawnBall = false;
        private bool spawnBallPressed = false;
        private bool destrouBallPressed = false;

        // Upgrades
        [SerializeField] private bool hasIncreasedJump = false;

        [Space]

        // Variables
        [SerializeField] private float moveSpeed = 3f;
        [SerializeField] private float jumpForce = 400f;
        [SerializeField] private float increasedJumpForce = 600f;
        [SerializeField] private float dashAmount = 4f;
        [SerializeField] private float hitRange = 3f;
        private float _directionX;
        private float hitCoolDown = 0.5f;

        private Vector3 _respawnPoint;

        private new void Start() {
            base.Start();
            _animationManager = GetComponent<PlayerAnimationManager>();
            MoveSpeed = moveSpeed;
            JumpForce = jumpForce;

            _respawnPoint = transform.position;
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
            canSpawnBall = _currentBall == null;
            
            if (!isDead) {
                Move(_directionX);
                
                if (IsGrounded && !isHitting) 
                    _animationManager.ChangeAnimationState(_directionX != 0 ? State.PlayerRunning : State.PlayerIdle);

                if (isJumpPressed && IsGrounded && !isHitting) {
                    isJumpPressed = false;
                    if (!isJumping) {
                        isJumping = true;
                        _animationManager.ChangeAnimationState(State.PlayerJumping);

                        if (hasIncreasedJump) Jump(increasedJumpForce);
                        else Jump();
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

                if (spawnBallPressed && canSpawnBall && !isJumping) {
                    spawnBallPressed = false;
                    var t = transform.position;
                    var spawnPosition = new Vector3(IsFacingRight ? t.x + 1 : t.x - 1, t.y, t.z);

                    _currentBall = Instantiate(ball, spawnPosition, quaternion.identity);
                }

                if (destrouBallPressed) {
                    destrouBallPressed = false;
                    if (_currentBall == null) return;
                    Destroy(_currentBall);
                }
            }
            else {
                _animationManager.ChangeAnimationState(State.PlayerFlatten);
            }
        }

        private void GetInput() {
            _directionX = Input.GetAxis("Horizontal");

            if (Input.GetButtonDown("Jump"))
                isJumpPressed = true;

            if (Input.GetMouseButton(0))
                isHitPressed = true;

            if (Input.GetKeyDown(KeyCode.Q))
                spawnBallPressed = true;

            if (Input.GetKeyDown(KeyCode.E))
                destrouBallPressed = true;
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

                if (obj.GetComponent<Ball>()) {
                    obj.GetComponent<Ball>().Move(IsFacingRight);
                }
            }
        }

        public override void TakeDamage(int damage) {
            currentHealth -= damage;
            if (currentHealth <= damage) {
                isDead = true;
                Invoke(nameof(ResetPlayer), 1f);
            }
        }

        private void ResetPlayer() {
            transform.position = _respawnPoint;
            isDead = false;
            isIdle = true;
            isRunning = false;
            isJumping = false;
            isHitting = false;
            hasIncreasedJump = false;
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