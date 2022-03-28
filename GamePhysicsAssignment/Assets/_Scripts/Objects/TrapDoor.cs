using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Objects {
    public class TrapDoor : MonoBehaviour {

        private Animator _animator;
        private BoxCollider2D _boxCollider;
        private const string TrapDoorUp = "trapDoorUp";
        private const string TrapDoorDown = "trapDoorDown";

        [SerializeField] private bool isUp = false;

        private void Start() {
            _animator = GetComponent<Animator>();
            _boxCollider = GetComponent<BoxCollider2D>();
            _boxCollider.enabled = false;
        }

        private void Update() {
            if (isUp) 
                SetTrapDoorUp();
            
            if (!isUp)
                SetTrapDoorDown();
        }

        public void SetTrapDoorUp() {
            isUp = true;
            _animator.Play(TrapDoorUp);
            _boxCollider.enabled = true;
        }

        public void SetTrapDoorDown() {
            isUp = false;
            _animator.Play(TrapDoorDown);
            _boxCollider.enabled = false;
        }
    }
}