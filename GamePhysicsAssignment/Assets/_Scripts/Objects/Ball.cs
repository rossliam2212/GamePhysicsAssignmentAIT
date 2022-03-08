using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Objects {
    public class Ball : MonoBehaviour {

        private Rigidbody2D rb;
        [SerializeField] private float moveSpeed = 5f;

        private void Start() {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update() { }

        public void Move(bool playerFacingRight) {
            var speed = moveSpeed * 100;
            rb.AddForce(playerFacingRight ? new Vector2(speed, 0f) : new Vector2(-speed, 0f));
        }

        private void OnCollisionEnter2D(Collision2D other) {
            if (other.gameObject.GetComponent<TurnableSlant>()) {
                var slantRotation = other.gameObject.GetComponent<TurnableSlant>().GetRotation();
                //print($"Collision with slant, Slant Rotation: {slantRotation}");
            }
        }
    }
}