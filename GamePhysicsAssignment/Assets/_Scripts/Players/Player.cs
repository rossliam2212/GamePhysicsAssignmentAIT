using System;
using System.Collections;
using Controllers;
using Objects;
using Unity.Mathematics;
using UnityEngine;

namespace Players {
    public class Player : Character {
        
        // Components/Game Objects
        private PlayerAnimationManager _animationManager;
        private GameUIController _gameUIController;
        
        [SerializeField] private Transform hitPoint;
        [SerializeField] private LayerMask interactablesLayer;
        
        [SerializeField] private GameObject ball;
        private GameObject _currentBall;

        [Space]

        // Bool Flags
        private bool isIdle = true;
        private bool isRunning = false;
        
        private bool isFlattened = false;

        private bool _isJumpPressed = false;
        private bool _isJumping = false;
        
        private bool _isHitPressed = false;
        private bool _isHitting = false;

        private bool _hitCoolDownTimer = false;
        
        private bool _canSpawnBall = false;
        private bool _spawnBallPressed = false;
        private bool _destroyBallPressed = false;

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
        private float _hitCoolDown = 0.5f;

        private int _lives = 3;

        private Vector3 _respawnPoint;

        private new void Start() {
            base.Start();
            _animationManager = GetComponent<PlayerAnimationManager>();
            _gameUIController = FindObjectOfType(typeof(GameUIController)) as GameUIController;
            
            MoveSpeed = moveSpeed;
            JumpForce = jumpForce;

            _respawnPoint = transform.position;
        }

        private void Update() {
            if (isDead) return;
            
            GetInput();

            if (_hitCoolDownTimer) {
                _hitCoolDown -= Time.deltaTime;
                if (_hitCoolDown <= 0f) {
                    _hitCoolDown = 0.5f;
                    _hitCoolDownTimer = false;
                    _isHitting = false;
                }
            }
        }

        private void FixedUpdate() {
            if (isDead || _lives <= 0) return;
            
            _canSpawnBall = _currentBall == null; 
            if (!isFlattened) {
                Move(_directionX);

                if (IsGrounded && !_isHitting)
                    _animationManager.ChangeAnimationState(_directionX != 0 ? State.PlayerRunning : State.PlayerIdle);

                if (_isJumpPressed && IsGrounded && !_isHitting) {
                    _isJumpPressed = false;
                    if (!_isJumping) {
                        _isJumping = true;
                        _animationManager.ChangeAnimationState(State.PlayerJumping);

                        if (hasIncreasedJump) Jump(increasedJumpForce);
                        else Jump();
                    }
                }

                if (_isHitPressed && IsGrounded && !_isJumping) {
                    _isHitPressed = false;
                    if (!_isHitting) {
                        _isHitting = true;
                        _animationManager.ChangeAnimationState(State.PlayerHitting);
                        CheckHit();
                        _hitCoolDownTimer = true;
                    }
                }

                if (_spawnBallPressed && _canSpawnBall && !_isJumping) {
                    _spawnBallPressed = false;
                    var t = transform.position;
                    var spawnPosition = new Vector3(IsFacingRight ? t.x + 1 : t.x - 1, t.y, t.z);

                    _currentBall = Instantiate(ball, spawnPosition, Quaternion.identity);
                }

                if (_destroyBallPressed) {
                    _destroyBallPressed = false;
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
                _isJumpPressed = true;

            if (Input.GetMouseButton(0))
                _isHitPressed = true;

            if (Input.GetKeyDown(KeyCode.Q))
                _spawnBallPressed = true;

            if (Input.GetKeyDown(KeyCode.E))
                _destroyBallPressed = true;
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
            if (isFlattened || isDead || _lives <= 0) return;
            
            currentHealth -= damage;
            if (currentHealth <= damage) {
                isFlattened = true;
                _lives--;
                if (_lives >= 1) {
                    Invoke(nameof(ResetPlayer), 1f);
                    print("Calling reset player");
                }
                else {
                    isDead = true;
                    Destroy(gameObject);
                    _gameUIController.SetGameOverUI();
                }
            }
        }

        private void ResetPlayer() {
            transform.position = _respawnPoint;
            isFlattened = false;
            isIdle = true;
            isRunning = false;
            _isJumping = false;
            _isHitting = false;
            hasIncreasedJump = false;
        }
        
        private void OnDrawGizmosSelected() {
            if (hitPoint == null) return;
            Gizmos.DrawWireSphere(hitPoint.position, hitRange);
        }
        
        public bool IsJumping {
            get => _isJumping;
            set => _isJumping = value;
        }

        public int Lives {
            get => _lives;
            set => _lives = value;
        }

        public bool CanSpawnBall {
            get => _canSpawnBall;
        }
    }
}