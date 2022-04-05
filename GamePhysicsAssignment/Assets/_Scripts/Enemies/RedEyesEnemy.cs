using System.Collections;
using System.Collections.Generic;
using Players;
using UnityEngine;

namespace Enemies {
    public class RedEyesEnemy : Enemy {

        [SerializeField] private float moveSpeed = 1f;
        [SerializeField] private float shootRange = 5f;
        private float _shootCoolDown = 1.5f;

        private bool _isShooting = false;
        private bool _shootCoolDownTimer = false;

        private float _stunnedCoolDown = 5f;
        private bool _stunnedCoolDownTimer = false;

        private const string RedEyesIdle = "redEyes_idle";
        private const string RedEyesWalk = "redEyes_walk";

        private new void Start() {
            ShootRange = shootRange;
            currentAnimationState = RedEyesIdle;
            MoveSpeed = moveSpeed;
            base.Start();
        }

        private void Update() {
            switch (currentState) {
                default:
                case EnemyState.Patrol:
                    if (IsStunned) {
                        currentState = EnemyState.Stunned;
                        _stunnedCoolDownTimer = true;
                    }
                    else if (InShootRange())
                        currentState = EnemyState.Shoot;
                    else {
                        Patrol();
                        ChangeAnimationState(RedEyesWalk);
                    }
                    break;
                
                case EnemyState.Shoot:
                    if (IsStunned) {
                        currentState = EnemyState.Stunned;
                        _stunnedCoolDownTimer = true;
                    }
                    else if (InShootRange()) {
                        ChangeAnimationState(RedEyesIdle);
                        Flip(Player.transform.position.x > transform.position.x ? Right : Left);
                        if (!_isShooting) {
                            _isShooting = true;
                            // Shoot();
                            _shootCoolDownTimer = true;
                        }
                    }
                    else 
                        currentState = EnemyState.Patrol;
                    break;
                
                case EnemyState.Stunned:
                    if (!IsStunned) {
                        if (InShootRange())
                            currentState = EnemyState.Shoot;
                        else
                            currentState = EnemyState.Patrol;
                    }
                    else {
                        ChangeAnimationState(RedEyesIdle);
                    }
                    break;
            }
            
            if (_shootCoolDownTimer) {
                _shootCoolDown -= Time.deltaTime;
                if (_shootCoolDown <= 0f) {
                    _shootCoolDownTimer = false;
                    _isShooting = false;
                    _shootCoolDown = 1.5f;
                }
            }

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