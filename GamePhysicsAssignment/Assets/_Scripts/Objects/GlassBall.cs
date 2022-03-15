using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Objects {
    public class GlassBall : MonoBehaviour {

        public Event onFullEvent;

        private Animator _animator;
        private const string glassBallFill = "glassBallFill";
        private const string glassBallEmpty = "glassBallEmpty";

        private bool isFull = false;

        private void Start() {
            _animator = GetComponent<Animator>();
        }

        private void OnCollisionEnter2D(Collision2D other) {
            if (other.gameObject.CompareTag("Ball")) {
                _animator.Play(glassBallFill);
                isFull = true;
                Destroy(other.gameObject);
            }
        }
    }
}