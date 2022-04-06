using UnityEngine;

namespace Objects {
    public class RedDotBlock : MonoBehaviour {

        private Animator _animator;
        
        // Animation States
        private const string RedDotBlockStill = "redDotBlockStill";
        private const string RedDotBlockFilled = "redDotBlock";

        private bool _isFilledRed = false;

        // Initialization
        private void Start() {
            _animator = GetComponent<Animator>();
            _animator.Play(RedDotBlockStill);
        }
        
        // Collision checking with the ball
        private void OnCollisionEnter2D(Collision2D other) {
            if (other.gameObject.CompareTag("Ball")) {
                _isFilledRed = true;
                _animator.Play(RedDotBlockFilled);
                Destroy(other.gameObject);
            }
        }
        
        /// <summary>
        /// Property for _isFilledRed.
        /// </summary>
        public bool IsFilledRed { get => _isFilledRed; }
    }
}