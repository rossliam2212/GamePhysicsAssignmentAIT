using UnityEngine;

namespace Players {
    public class PlayerCollisions : MonoBehaviour {
        
        // Components/Game Objects
        private Player _player;
        [SerializeField] private LayerMask whatIsGround;

        // Initialization
        private void Start() { _player = GetComponent<Player>(); }

        private void OnCollisionEnter2D(Collision2D other) {
            // Colliding with ground
            if ((whatIsGround.value & (1 << other.transform.gameObject.layer)) > 0) {
                _player.IsJumping = false;
                _player.SetIsGrounded(true);
            }
            
            // Colliding with the crystal
            if (other.gameObject.CompareTag("Crystal")) {
                _player.HasCrystal = true;
                Destroy(other.gameObject);
            }

            // Colliding with the boulder
            // Colliding with spikes
            // Falling out of the map
            // Colliding with the Orange Force Field
            // Colliding with the Blue Force Field
            if (other.gameObject.CompareTag("Boulder") ||
                other.gameObject.CompareTag("Spikes") ||
                other.gameObject.CompareTag("OutOfBounds") ||
                other.gameObject.CompareTag("OrangeFF") ||
                other.gameObject.CompareTag("BlueFF")) {
                _player.TakeDamage(100);
            }

            Physics2D.IgnoreLayerCollision(9, 10, true);
        }

        // Used in Robot Level (Level 2) to set the new spawn point after the place has passed the first area.
        private void OnTriggerEnter2D(Collider2D other) {
            if (other.gameObject.CompareTag("NewSpawnPoint")) {
                _player.RespawnPosition = transform.position;
            }
        }
    }
}