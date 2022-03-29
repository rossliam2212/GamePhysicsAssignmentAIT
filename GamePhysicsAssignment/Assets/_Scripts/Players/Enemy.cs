using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Players {

    public enum EnemyState {
        Idle,
        Patrol,
        Chase,
        Shoot,
        Stunned
    }

    public class Enemy : Character {

        protected float ChaseRange;
        protected float ShootRange;
        protected int ShootDamageAmount;

        protected bool IsStunned = false;
        private float stunnedCoolDown = 5f;
        private bool stunnedCoolDownTimer = false;

        protected string CurrentAnimationState;
        protected EnemyState CurrentState;
        [SerializeField] private List<Transform> patrolPoints;
        private int currentWayPoint = 0;

        protected Player Player;
        [SerializeField] private Transform shootPoint;
        [SerializeField] private GameObject bullet;

        private void Awake() {
            Player = FindObjectOfType(typeof(Player)) as Player;
        }

        private new void Start() {
            base.Start();
            CurrentState = EnemyState.Idle;
            ChangeAnimationState(CurrentAnimationState);
        }

        private void Update() { }

        protected void Patrol() { }

        protected void Chase() { }

        protected bool InChaseRange() {
            if (Player == null) return false;
            return Mathf.Abs(Player.transform.position.x - transform.position.x) < ChaseRange;
        }

        protected bool InShootRange() {
            if (Player == null) return false;
            return Mathf.Abs(Player.transform.position.x - transform.position.x) < ShootRange;
        }

        protected void Shoot() {
            if (shootPoint == null) return;
            Instantiate(bullet, shootPoint.position, Quaternion.identity);
            CheckShoot();
        }

        protected void ChangeAnimationState(string newState) {
            if (newState == CurrentAnimationState) return;
            Animator.Play(newState);
            CurrentAnimationState = newState;
        }

        private void CheckShoot() { }
    }
}