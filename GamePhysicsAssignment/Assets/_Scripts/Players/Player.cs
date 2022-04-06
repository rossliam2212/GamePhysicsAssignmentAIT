using Controllers;
using Objects;
using UnityEngine;

namespace Players {
    public class Player : Character {
        
        // Components/Game Objects
        private PlayerAnimationManager _animationManager;
        private GameUIController _gameUIController;

        [SerializeField] private Transform groundCheck;
        [SerializeField] private LayerMask whatIsGround;
        [SerializeField] private bool onGround = true;
        [SerializeField] private float onGroundCheckRadius = 2f;
        
        
        [SerializeField] private Transform hitPoint;
        [SerializeField] private LayerMask interactablesLayer;
        
        [SerializeField] private GameObject ball;
        private GameObject _currentBall;

        [Space]

        // Bool Flags
        private bool _isIdle = true;
        private bool _isRunning = false;
        
        private bool _isFlattened = false;

        private bool _isJumpPressed = false;
        private bool _isJumping = false;
        
        private bool _isHitPressed = false;
        private bool _isHitting = false;

        private bool _hitCoolDownTimer = false;
        
        private bool _canSpawnBall = false;
        private bool _spawnBallPressed = false;
        private bool _destroyBallPressed = false;

        private bool _hasCrystal = false;

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

        // Initialization
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
            CheckOnGround();

            if (_hitCoolDownTimer) {
                _hitCoolDown -= Time.deltaTime;
                if (_hitCoolDown <= 0f) {
                    _hitCoolDown = 0.5f;
                    _hitCoolDownTimer = false;
                    _isHitting = false;
                }
            }
        }

        // Handles all player movement & animation changes.
        private void FixedUpdate() {
            if (isDead || _lives <= 0) return;
            
            _canSpawnBall = _currentBall == null; 
            
            if (!_isFlattened) {
                if (onGround)
                    Move(_directionX);
                else 
                    if (_directionX != 0) Move(_directionX); // To make the surface effector on the convayer belt work
                
                
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

        /// <summary>
        /// Handles all keyboard/mouse input from the player.
        /// </summary>
        private void GetInput() {
            _directionX = Input.GetAxis("Horizontal");

            if (Input.GetButtonDown("Jump"))
                _isJumpPressed = true;

            // Left Mouse - Makes the player hit.
            if (Input.GetMouseButton(0))
                _isHitPressed = true;

            // Q - Spawns the ball.
            if (Input.GetKeyDown(KeyCode.Q))
                _spawnBallPressed = true;
            
            // E - Destroys the ball if it is spawned and returns it to the players inventory.
            if (Input.GetKeyDown(KeyCode.E))
                _destroyBallPressed = true;
        }

        /// <summary>
        /// Checks if the player has hit anything whenever hit has been pressed. 
        /// </summary>
        private void CheckHit() {
            var hit = Physics2D.OverlapCircleAll(hitPoint.position, hitRange, interactablesLayer);
            foreach (var obj in hit) {
                if (obj.GetComponent<TurnableSlant>()) {
                    obj.GetComponent<TurnableSlant>().Hit(true);
                }

                if (obj.GetComponent<Switch>()) {
                    obj.GetComponent<Switch>().FlipSwitch();
                }

                if (obj.GetComponent<Ball>()) {
                    obj.GetComponent<Ball>().Move(IsFacingRight);
                }
            }
        }

        /// <summary>
        /// Checks whether the player is currently standing on ground or not and keeps the onGround variable updated.
        /// Used so the surface effector on the convayer belts effects the player's rb.
        /// </summary>
        private void CheckOnGround() {
            var colliders = Physics2D.OverlapCircleAll(groundCheck.position, onGroundCheckRadius, whatIsGround);
            onGround = colliders.Length != 0;
        }

        /// <summary>
        /// Overridden method from Character base class.
        /// Deals damage to the player and kills the player once all lives have been lost.
        /// </summary>
        /// <param name="damage">The amount of damage to deal.</param>
        public override void TakeDamage(int damage) {
            if (_isFlattened || isDead || _lives <= 0) return;
            
            currentHealth -= damage;
            if (currentHealth <= damage) {
                _isFlattened = true;
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

        /// <summary>
        /// Resets the player after they have respawned.
        /// </summary>
        private void ResetPlayer() {
            transform.position = _respawnPoint;
            _isFlattened = false;
            _isIdle = true;
            _isRunning = false;
            _isJumping = false;
            _isHitting = false;
            hasIncreasedJump = false;
        }
        
        /// <summary>
        /// Drawing radius' for the hitPoint & groundCheck. 
        /// </summary>
        private void OnDrawGizmosSelected() {
            if (hitPoint == null) return;
            Gizmos.DrawWireSphere(hitPoint.position, hitRange);

            if (groundCheck == null) return;
            Gizmos.DrawWireSphere(groundCheck.position, onGroundCheckRadius);
        }
        
        /// <summary>
        /// Property for _isJumping.
        /// </summary>
        public bool IsJumping {
            get => _isJumping;
            set => _isJumping = value;
        }

        /// <summary>
        /// Property for _lives.
        /// </summary>
        public int Lives {
            get => _lives;
            set => _lives = value;
        }

        /// <summary>
        /// Property for _canSpawnBall.
        /// </summary>
        public bool CanSpawnBall {
            get => _canSpawnBall;
        }

        /// <summary>
        /// Property for _hasCrystal.
        /// </summary>
        public bool HasCrystal {
            get => _hasCrystal;
            set => _hasCrystal = value;
        }
        
        /// <summary>
        /// Property for _respawnPoint.
        /// </summary>
        public Vector3 RespawnPosition { set => _respawnPoint = value; }
    }
}