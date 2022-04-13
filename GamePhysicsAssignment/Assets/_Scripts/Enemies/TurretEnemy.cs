using Players;
using UnityEngine;

namespace Enemies {
    public class TurretEnemy : Enemy {

        // Variables
        [SerializeField] private float moveSpeed = 2f;
        [SerializeField] private float shootRange = 5f;
        private float _shootCoolDown = 2f;

        private bool _isShooting = false;
        private bool _shootCoolDownTimer = false;
        
        private float _stunnedCoolDown = 5f;
        private bool _stunnedCoolDownTimer = false;

        // Animation States
        private const string TurretEnemyIdle = "turretEnemy_idle";
        private const string TurretEnemyWalk = "turretEnemy_walk";
        
        // Initialization
        private new void Start() {
            ShootRange = shootRange;
            currentAnimationState = TurretEnemyIdle;
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
                        ChangeAnimationState(TurretEnemyWalk);
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
                        ChangeAnimationState(TurretEnemyIdle);
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
                        ChangeAnimationState(TurretEnemyIdle);
                    }
                    break;
            }
            
            // Shooting Cool Down Timer
            if (_shootCoolDownTimer) {
                _shootCoolDown -= Time.deltaTime;
                if (_shootCoolDown <= 0f) {
                    _shootCoolDownTimer = false;
                    _isShooting = false;
                    _shootCoolDown = 2f;
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