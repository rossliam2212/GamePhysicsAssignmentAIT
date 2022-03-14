using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Objects {
    public class GlassBall : MonoBehaviour {

        private Animator _animator;
        private const string glassBallFill = "glassBallFill";
        private const string glassBallEmpty = "glassBallEmpty";

        private void Start() {
            _animator = GetComponent<Animator>();
        }

        private void OnCollisionEnter2D(Collision2D other) {
            if (other.gameObject.CompareTag("Ball")) {
                _animator.Play(glassBallFill);
                Destroy(other.gameObject);
                
            }
        }
    }
}