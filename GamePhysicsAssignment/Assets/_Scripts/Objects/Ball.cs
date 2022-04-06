using UnityEngine;

namespace Objects {
    public class Ball : MonoBehaviour {

        private Rigidbody2D _rb;
        private Vector3 _lastVelocity;
        [SerializeField] private float moveSpeed = 5f;

        // Initialization
        private void Start() {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update() {
            _lastVelocity = _rb.velocity;
        }

        /// <summary>
        /// Moves the ball when the player hits it in the direction the player is facing.
        /// </summary>
        /// <param name="playerFacingRight"></param>
        public void Move(bool playerFacingRight) {
            var speed = moveSpeed * 100;
            _rb.AddForce(playerFacingRight ? new Vector2(speed, 0f) : new Vector2(-speed, 0f));
        }

        // Collision Checking
        private void OnCollisionEnter2D(Collision2D other) {
            // Collision with the slant
            if (other.gameObject.CompareTag("SlantCollider")) {
                other.gameObject.GetComponentInParent<TurnableSlant>().Hit();
                var speed = _lastVelocity.magnitude;
                var direction = Vector3.Reflect(_lastVelocity.normalized, other.contacts[0].normal);
                _rb.velocity = direction * Mathf.Max(speed, 0f);
            }

            if (other.gameObject.CompareTag("EnemyBullet") || 
                other.gameObject.CompareTag("TurretEnemy") ||
                other .gameObject.CompareTag("RedEyes") || 
                other.gameObject.CompareTag("YellowEyes") ||
                other.gameObject.CompareTag("OrangeFF") ||
                other.gameObject.CompareTag("BlueFF") ||
                other.gameObject.CompareTag("Spikes")) {
                Destroy(gameObject);
            }
        }

        // Out of Bounds
        private void OnTriggerEnter2D(Collider2D other) {
            if (other.gameObject.CompareTag("OutOfBounds")) {
                Destroy(gameObject);
            }
        }
    }
}