using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Players {
    public class PlayerCollisions : MonoBehaviour {
        
        private Player _player;
        [SerializeField] private LayerMask whatIsGround;
        [SerializeField] private LayerMask enemyLayers;

        private void Start() {
            _player = GetComponent<Player>();
        }

        private void OnCollisionEnter2D(Collision2D other) {
            if ((whatIsGround.value & (1 << other.transform.gameObject.layer)) > 0) {
                print("Collision with ground");
                _player.IsJumping = false;
                _player.SetIsGrounded(true);
            }
        }
    }
}