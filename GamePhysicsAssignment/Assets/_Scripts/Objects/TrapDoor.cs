using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Objects {
    public class TrapDoor : MonoBehaviour {

        private Animator _animator;
        private BoxCollider2D _boxCollider;
        private const string TrapDoorUp = "trapDoorUp";
        private const string TrapDoorDown = "trapDoorDown";

        private bool _isUp = false;

        private void Start() {
            _animator = GetComponent<Animator>();
            _boxCollider = GetComponent<BoxCollider2D>();
            _boxCollider.enabled = false;
        }

        private void Update() { }

        public void SetTrapDoorUp() {
            _animator.Play(TrapDoorUp);
            _boxCollider.enabled = true;
        }

        public void SetTrapDoorDown() {
            _animator.Play(TrapDoorDown);
            _boxCollider.enabled = false;
        }
    }
}