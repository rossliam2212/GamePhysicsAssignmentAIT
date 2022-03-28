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
        
        private new void Start() {
            base.Start();
            ShootRange = shootRange;
        }

        private void Update() {
            switch (CurrentState) {
                default:
                case EnemyState.Idle:
                    if (InShootRange()) 
                        CurrentState = EnemyState.Shooting;
                    break;
                case EnemyState.Shooting:
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