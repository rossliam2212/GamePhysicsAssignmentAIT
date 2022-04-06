using UnityEngine;
using UnityEngine.Events;

namespace Objects {
    public class GlassBall : MonoBehaviour {

        // Glass Ball Event
        [SerializeField] private UnityEvent onFill;

        private Animator _animator;
        
        // Animation States
        private const string glassBallFill = "glassBallFill";
        private const string glassBallEmpty = "glassBallEmpty";

        // Initialization
        private void Start() {
            _animator = GetComponent<Animator>();
        }

        // Collision Checking with the ball
        private void OnCollisionEnter2D(Collision2D other) {
            if (other.gameObject.CompareTag("Ball")) {
                _animator.Play(glassBallFill);
                onFill.Invoke();
                Destroy(other.gameObject);
            }
        }
    }
}