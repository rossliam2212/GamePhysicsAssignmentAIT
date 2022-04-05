using System.Collections;
using System.Collections.Generic;
using Players;
using UnityEngine;

namespace Enemies {
    public class YellowEyesEnemy : Enemy {

        [SerializeField] private float moveSpeed = 1f;
        [SerializeField] private float shootRange = 5f;
        private float _shootCoolDown = 2f;

        private bool _isShooting = false;
        private bool _shootCoolDownTimer = false;

        private float _stunnedCoolDown = 5f;
        private bool _stunnedCoolDownTimer = false;

        private const string YellowEyesIdle = "yellowEyes_idle";
        private const string YellowEyesWalk = "yellowEyes_walk";

        private new void Start() {
            ShootRange = shootRange;
            currentAnimationState = YellowEyesIdle;
            MoveSpeed = moveSpeed;
            base.Start();
        }

        private void Update() {
            switch (currentState) {
                default:
                case EnemyState.Patrol:
                    break;
                
                case EnemyState.Shoot:
                    break;
                
                case EnemyState.Stunned:
                    break;
            }
        }
    }
}