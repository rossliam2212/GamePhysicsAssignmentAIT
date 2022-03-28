using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Players {

    public enum EnemyState {
        Idle,
        Shooting
    }
    
    public class Enemy : Character {

        protected float ShootRange;
        protected int ShootDamageAmount;

        protected EnemyState CurrentState;
        
        protected Player Player;
        [SerializeField] private Transform shootPoint;
        [SerializeField] private GameObject bullet;

        private void Awake() {
            Player = FindObjectOfType(typeof(Player)) as Player;
        }

        private new void Start() {
            base.Start();
            CurrentState = EnemyState.Idle;
        }

        private void Update() { }

        protected bool InShootRange() {
            if (Player == null) return false;
            return Mathf.Abs(Player.transform.position.x - transform.position.x) < ShootRange;
        }

        protected void Shoot() {
            if (shootPoint == null) return;
            Instantiate(bullet, shootPoint.position, Quaternion.identity);
            CheckShoot();
        }

        private void CheckShoot() {

        }
    }
}