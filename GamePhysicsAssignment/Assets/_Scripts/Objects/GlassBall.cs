using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Objects {
    public class GlassBall : MonoBehaviour {

        [SerializeField] private UnityEvent onFill;

        private Animator _animator;
        private const string glassBallFill = "glassBallFill";
        private const string glassBallEmpty = "glassBallEmpty";

        private void Start() {
            _animator = GetComponent<Animator>();
        }

        private void OnCollisionEnter2D(Collision2D other) {
            if (other.gameObject.CompareTag("Ball")) {
                _animator.Play(glassBallFill);
                onFill.Invoke();
                Destroy(other.gameObject);
            }
        }
    }
}