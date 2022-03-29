using System.Collections;
using System.Collections.Generic;
using Players;
using UnityEngine;

namespace Enemies {
    public class TurretEnemy : Enemy {

        [SerializeField] private float shootRange = 5f;
        private float _shootCoolDown = 2f;

        private bool _isShooting = false;
        private bool _shootCoolDownTimer = false;

        private const string TurretEnemyIdle = "turretEnemy_idle";
        private const string TurretEnemyWalk = "turretEnemy_walk";
        
        private new void Start() {
            ShootRange = shootRange;
            CurrentAnimationState = TurretEnemyIdle;
            base.Start();
        }

        private void Update() {
            switch (CurrentState) {
                default:
                case EnemyState.Idle:
                    if (InShootRange()) 
                        CurrentState = EnemyState.Shoot;
                    break;
                case EnemyState.Shoot:
                    if (InShootRange()) {
                        Flip(Player.transform.position.x > transform.position.x ? Right : Left);
                        if (!_isShooting) {
                            _isShooting = true;
                            Shoot();
                            _shootCoolDownTimer = true;
                        }
                    }
                    else CurrentState = EnemyState.Idle;
                    break;
            }
            
            if (_shootCoolDownTimer) {
                _shootCoolDown -= Time.deltaTime;
                if (_shootCoolDown <= 0f) {
                    _shootCoolDownTimer = false;
                    _isShooting = false;
                    _shootCoolDown = 2f;
                }
            }
        }
    }
}