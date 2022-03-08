using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Objects {
    public class TurnableSlant : MonoBehaviour {

        private SpriteRenderer _spriteRenderer;
        private Sprite whiteSlantSprite;
        [SerializeField] private Sprite redSlantSprite;

        private int currentDirection = 1;
        private const int Up = 1;
        private const int Right = 2;
        private const int Down = 3;
        private const int Left = 4;

        private void Start() {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            whiteSlantSprite = _spriteRenderer.sprite;
        }

        private void Update() { }

        public void Hit() {
            _spriteRenderer.sprite = redSlantSprite;
            Invoke(nameof(ResetSprite), 0.2f);
            Rotate();
        }

        private void Rotate() {
            transform.Rotate(0f, 0f, -90);
            currentDirection++;

            if (currentDirection > 4) currentDirection = 1;
        }

        private void ResetSprite() {
            _spriteRenderer.sprite = whiteSlantSprite;
        }

        public float GetDirection() {
            return currentDirection;
        }
    }
}