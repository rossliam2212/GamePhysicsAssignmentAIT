using UnityEngine;

namespace Objects {
    public class Boulder : MonoBehaviour {

        // Out of Bounds
        private void OnTriggerEnter2D(Collider2D other) {
            if (other.gameObject.CompareTag("OutOfBounds"))
                Destroy(gameObject);
        }
    }
}