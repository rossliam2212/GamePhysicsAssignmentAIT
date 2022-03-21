using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Players {
    public class PlayerCollisions : MonoBehaviour {
        
        private Player _player;
        [SerializeField] private LayerMask whatIsGround;
        [SerializeField] private LayerMask enemyLayers;

        private void Start() { _player = GetComponent<Player>(); }

        private void OnCollisionEnter2D(Collision2D other) {
            if ((whatIsGround.value & (1 << other.transform.gameObject.layer)) > 0) {
                print("Collision with ground");
                _player.IsJumping = false;
                _player.SetIsGrounded(true);
            }

            if (other.gameObject.CompareTag("Boulder")) {
                _player.TakeDamage(100);
            }

            if (other.gameObject.CompareTag("Spikes")) {
                _player.TakeDamage(100);
            }
            
            Physics2D.IgnoreLayerCollision(9, 10, true);
        }

        private void OnTriggerEnter2D(Collider2D other) { }
    }
}