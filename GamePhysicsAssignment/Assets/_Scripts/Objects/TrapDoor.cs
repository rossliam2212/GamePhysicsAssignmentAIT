using UnityEngine;

namespace Objects {
    public class TrapDoor : MonoBehaviour {

        // Components
        private Animator _animator;
        private BoxCollider2D _boxCollider;
        
        // Animation States
        private const string TrapDoorUp = "trapDoorUp";
        private const string TrapDoorDown = "trapDoorDown";

        [SerializeField] private bool isUp = false;

        // Initialization
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

        /// <summary>
        /// Sets the trap down to its up state.
        /// </summary>
        public void SetTrapDoorUp() {
            isUp = true;
            _animator.Play(TrapDoorUp);
            _boxCollider.enabled = true;
        }

        /// <summary>
        /// Sets the trap door to its down state.
        /// </summary>
        public void SetTrapDoorDown() {
            isUp = false;
            _animator.Play(TrapDoorDown);
            _boxCollider.enabled = false;
        }
    }
}