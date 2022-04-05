using System;
using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;

namespace Players {

    // Enemy states
    public enum EnemyState {
        Idle,
        Patrol,
        Chase,
        Shoot,
        Stunned
    }

    public class Enemy : Character {

        // Variables
        protected float ChaseRange;
        protected float ShootRange;
        private const float YRange = 0.5f;
        protected int ShootDamageAmount;

        protected bool IsStunned = false;
        protected bool IsAgro = false;

        // Animation
        [SerializeField] protected string currentAnimationState;
        [SerializeField] protected EnemyState currentState;
        
        // Patrolling
        [SerializeField] private Transform[] patrolPoints;
        private int _currentWayPoint = 0;
        private bool _once = false;
        private const float WaitTime = 1.5f;
        
        private const double Tolerance = 1.05f;

        // Components/Game Objects
        protected Player Player;
        [SerializeField] private Transform shootPoint;
        protected EnemyWeapon Weapon;
        
        [SerializeField] private Transform aboveHeadPoint;
        [SerializeField] private GameObject questionMark;
        [SerializeField] private GameObject exclamationMark;

        // Initialization
        private void Awake() {
            Player = FindObjectOfType(typeof(Player)) as Player;
            Weapon = GetComponent<EnemyWeapon>();
        }

        // Initialization
        private new void Start() {
            base.Start();
            currentState = EnemyState.Idle;
            ChangeAnimationState(currentAnimationState);
        }


        /// <summary>
        /// This method blah blah 
        /// </summary>
        protected void Patrol() {
            var wayPoint = patrolPoints[_currentWayPoint];
            Chase(wayPoint.position);
            
            if (Vector2.Distance(transform.position, wayPoint.position) < Tolerance) {
                if (_once) return;
                _once = true;
                StartCoroutine(Wait());
            }
        }

        /// <summary>
        /// This Coroutine is used with the Patrol() method. The enemy will wait at each patrol point for a fixed time before moving to the next point.
        /// </summary>
        /// <returns></returns>
        private IEnumerator Wait() {
            yield return new WaitForSeconds(WaitTime);
            
            if (_currentWayPoint + 1 < patrolPoints.Length) 
                _currentWayPoint++;
            else 
                _currentWayPoint = 0;

            Chase(patrolPoints[_currentWayPoint].position);
            _once = false;
        }
        
        /// <summary>
        /// Makes the enemy chase a specific target or position.
        /// </summary>
        /// <param name="target">The target/position to chase.</param>
        protected void Chase(Vector3 target) {
            var speed = MoveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, speed);
        }

        /// <summary>
        /// Checks whether the player is within chase range to the enemy.
        /// </summary>
        /// <returns>True if the player is within chase range, False otherwise.</returns>
        protected bool InChaseRange() {
            if (Player == null) return false;
            return Mathf.Abs(Player.transform.position.x - transform.position.x) < ChaseRange;
        }

        /// <summary>
        /// Checks whether the player is within shoot range to the enemy.
        /// </summary>
        /// <returns>True if the player is within shoot range, False otherwise.</returns>
        protected bool InShootRange() {
            if (Player == null) return false;
            return Mathf.Abs(Player.transform.position.x - transform.position.x) < ShootRange;
        }

        /// <summary>
        /// Checks whether the player is within a certain distance to the enemy on the y-axis for shooting. 
        /// </summary>
        /// <returns>True if the player is within the range on the y-axis, False otherwise.</returns>
        protected bool InYRange() {
            if (Player == null) return false;
            return Mathf.Abs(Player.transform.position.y - transform.position.y) < YRange;
        }
        
        /// <summary>
        /// Changes the enemies current animation state.
        /// </summary>
        /// <param name="newState">The new state to change to.</param>
        protected void ChangeAnimationState(string newState) {
            if (newState == currentAnimationState) return;
            Animator.Play(newState);
            currentAnimationState = newState;
        }

        /// <summary>
        /// Used to destroy the exclamation mark that spawns over the enemies head when they are no longer agro.
        /// </summary>
        /// <param name="objToDestroy"></param>
        protected void DestroyObject(GameObject objToDestroy) {
            Destroy(objToDestroy);
        }

        // Collision Checking
        private void OnCollisionEnter2D(Collision2D other) {
            // Spawning the question mark above the enemies when they become stunned.
            if (other.gameObject.CompareTag("Ball") && !IsStunned) {
                IsStunned = true;
                var questionMarkObj = Instantiate(questionMark, aboveHeadPoint.position, Quaternion.identity);
                Destroy(questionMarkObj, 5f);
            }
        }
    }
}