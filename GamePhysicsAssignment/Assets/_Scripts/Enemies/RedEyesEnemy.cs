using Players;
using UnityEngine;

namespace Enemies {
    public class RedEyesEnemy : Enemy {

        // Variables
        [SerializeField] private float moveSpeed = 1f;
        [SerializeField] private float shootRange = 5f;
        private float _shootCoolDown = 0.75f;

        private bool _isShooting = false;
        private bool _shootCoolDownTimer = false;

        private float _stunnedCoolDown = 5f;
        private bool _stunnedCoolDownTimer = false;

        // Animation States
        private const string RedEyesIdle = "redEyes_idle";
        private const string RedEyesWalk = "redEyes_walk";

        // Initialization
        private new void Start() {
            ShootRange = shootRange;
            currentAnimationState = RedEyesIdle;
            MoveSpeed = moveSpeed;
            base.Start();
        }

        private void Update() {
            // Spawning the exclamation mark above the enemies head when in agro range
            if (IsAgro) {
                if (!ExclamationMarkSpawned) {
                    ExclamationMarkSpawned = true;
                    CurrentExclamationMark = Instantiate(exclamationMark, aboveHeadPoint.position, Quaternion.identity);
                }
            }
            else {
                IsAgro = false;
                ExclamationMarkSpawned = false;
                Destroy(CurrentExclamationMark);
            }
            
            // Enemy States
            switch (currentState) {
                default:
                case EnemyState.Patrol:
                    if (IsStunned) {
                        currentState = EnemyState.Stunned;
                        _stunnedCoolDownTimer = true;
                    }
                    else if (InShootRange() && InYRange())
                        currentState = EnemyState.Shoot;
                    else {
                        Patrol();
                        ChangeAnimationState(RedEyesWalk);
                    }
                    break;
                
                case EnemyState.Shoot:
                    if (IsStunned) {
                        IsAgro = false;
                        currentState = EnemyState.Stunned;
                        _stunnedCoolDownTimer = true;
                    }
                    else if (InShootRange() && InYRange()) {
                        IsAgro = true;
                        ChangeAnimationState(RedEyesIdle);
                        Flip(Player.transform.position.x > transform.position.x ? Right : Left);
                        if (!_isShooting) {
                            _isShooting = true;
                            Weapon.Shoot();
                            _shootCoolDownTimer = true;
                        }
                    }
                    else {
                        IsAgro = false;
                        currentState = EnemyState.Patrol;
                    }
                    break;
                
                case EnemyState.Stunned:
                    if (!IsStunned) {
                        if (InShootRange() && InYRange())
                            currentState = EnemyState.Shoot;
                        else
                            currentState = EnemyState.Patrol;
                    }
                    else {
                        ChangeAnimationState(RedEyesIdle);
                    }
                    break;
            }
            
            // Shooting Cool Down Timer
            if (_shootCoolDownTimer) {
                _shootCoolDown -= Time.deltaTime;
                if (_shootCoolDown <= 0f) {
                    _shootCoolDownTimer = false;
                    _isShooting = false;
                    _shootCoolDown = 0.75f;
                }
            }

            // Stunned Cool Down Timer
            if (_stunnedCoolDownTimer) {
                _stunnedCoolDown -= Time.deltaTime;
                if (_stunnedCoolDown <= 0f) {
                    _stunnedCoolDownTimer = false;
                    IsStunned = false;
                    _stunnedCoolDown = 5f;
                }
            }
        }
    }
}