using UnityEngine;
using UnityEngine.Events;

namespace Objects {
    public class SmallButton : MonoBehaviour {
        
        // Small Button Event
        [SerializeField] private UnityEvent onPress;

        private SpriteRenderer _sr;
        [SerializeField] private Sprite smallButtonDown;
        [SerializeField] private bool isDown = false;

        // Initialization
        private void Start() {
            _sr = GetComponent<SpriteRenderer>();
        }

        // Collision checking with the player - Invokes the small button UnityEvent
        private void OnTriggerEnter2D(Collider2D other) {
            if (other.gameObject.CompareTag("Player")) {
                _sr.sprite = smallButtonDown;
                isDown = true;
                onPress.Invoke();
            }
        }
    }
}