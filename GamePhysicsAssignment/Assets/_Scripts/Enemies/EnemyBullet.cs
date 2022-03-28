using System;
using System.Collections;
using System.Collections.Generic;
using Players;
using UnityEngine;

namespace Enemies {
    public class EnemyBullet : MonoBehaviour {
        
        private Rigidbody2D _rb;
        private int _bulletDamageAmount = 20;
        private float _bulletSpeed = 5f;
        
        private void Start() {
            _rb = GetComponent<Rigidbody2D>();
            _rb.velocity = transform.right * _bulletSpeed;
        }

        private void Update() { }

        private void OnCollisionEnter2D(Collision2D other) {
            if (other.gameObject.GetComponent<Player>()) {
                other.gameObject.GetComponent<Player>().TakeDamage(_bulletDamageAmount);
            }

            if (other.gameObject.CompareTag("Slant") || other.gameObject.CompareTag("Boulder") ||
                other.gameObject.CompareTag("TrapDoor") ||
                other.gameObject.CompareTag("Walls")) {
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