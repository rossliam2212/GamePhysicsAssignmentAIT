using System;
using System.Collections;
using System.Collections.Generic;
using Players;
using UnityEngine;

namespace Enemies {
    public class EnemyBullet : MonoBehaviour {
        
        private Rigidbody2D _rb;
        private int bulletDamageAmount = 20;
        
        private void Start() {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update() { }

        private void OnCollisionEnter2D(Collision2D other) {
            if (other.gameObject.GetComponent<Player>()) {
                other.gameObject.GetComponent<Player>().TakeDamage(bulletDamageAmount);
            }
        }
    }
}