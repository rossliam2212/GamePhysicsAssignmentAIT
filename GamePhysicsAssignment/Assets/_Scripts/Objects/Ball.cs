using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Objects {
    public class Ball : MonoBehaviour {

        private Rigidbody2D _rb;
        private Vector3 _lastVelocity;
        [SerializeField] private float moveSpeed = 5f;

        private void Start() {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update() {
            _lastVelocity = _rb.velocity;
        }

        public void Move(bool playerFacingRight) {
            var speed = moveSpeed * 100;
            _rb.AddForce(playerFacingRight ? new Vector2(speed, 0f) : new Vector2(-speed, 0f));
        }

        private void Bounce(int direction) {
            var speed = moveSpeed * 100;
            switch (direction) {
                // Up
                case 1:
                    _rb.AddForce(new Vector2(0f, -speed));
                    break;
                // Right
                case 2:
                    _rb.AddForce(new Vector2(-speed, 0f));
                    break;
                // Down
                case 3:
                    _rb.AddForce(new Vector2(0f, speed));
                    break;
                // Left
                case 4:
                    _rb.AddForce(new Vector2(speed, 0f));
                    break;
            }
        }

        private void OnCollisionEnter2D(Collision2D other) {
            if (other.gameObject.CompareTag("SlantCollider")) {
                var speed = _lastVelocity.magnitude;
                var direction = Vector3.Reflect(_lastVelocity.normalized, other.contacts[0].normal);
                _rb.velocity = direction * Mathf.Max(speed, 0f);
            }

            if (other.gameObject.CompareTag("EnemyBullet") || other.gameObject.CompareTag("TurretEnemy")) {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.gameObject.CompareTag("OutOfBounds")) {
                Destroy(gameObject);
            }
        }
    }
}