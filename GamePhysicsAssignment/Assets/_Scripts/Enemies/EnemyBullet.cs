using Players;
using UnityEngine;

namespace Enemies {
    public class EnemyBullet : MonoBehaviour {
        
        private Rigidbody2D _rb;
        private int _bulletDamageAmount = 20;
        private float _bulletSpeed = 5f;
        
        // Initialization
        private void Start() {
            _rb = GetComponent<Rigidbody2D>();
            _rb.velocity = transform.right * _bulletSpeed;
        }

        // Collision Checking
        private void OnCollisionEnter2D(Collision2D other) {
            if (other.gameObject.GetComponent<Player>()) {
                other.gameObject.GetComponent<Player>().TakeDamage(_bulletDamageAmount); // Deals damage to the player
                Destroy(gameObject);
            }

            if (other.gameObject.CompareTag("Ball")) {
                Destroy(gameObject);
                Destroy(other.gameObject);
            }

            if (other.gameObject.CompareTag("Slant") || 
                other.gameObject.CompareTag("Boulder") ||
                other.gameObject.CompareTag("TrapDoor") ||
                other.gameObject.CompareTag("Walls") ||
                other.gameObject.CompareTag("EnemyBullet") ||
                other.gameObject.CompareTag("OrangeFF") ||
                other.gameObject.CompareTag("BlueFF") ||
                other.gameObject.layer == 6) {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.gameObject.CompareTag("OutOfBounds")) {
                Destroy(gameObject);
            }
        }
    }
}