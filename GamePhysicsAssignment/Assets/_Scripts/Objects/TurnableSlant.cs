using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Objects {
    public class TurnableSlant : MonoBehaviour {

        private SpriteRenderer _spriteRenderer;
        private Sprite _whiteSlantSprite;
        [SerializeField] private Sprite redSlantSprite;

        // Up - 1
        // Right - 2
        // Down - 3
        // Left - 4
        private int _currentDirection = 1;

        private void Start() {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _whiteSlantSprite = _spriteRenderer.sprite;
        }

        private void Update() { }

        public void Hit(bool rotate = false) {
            _spriteRenderer.sprite = redSlantSprite;
            Invoke(nameof(ResetSprite), 0.2f);
            
            if (rotate)
                Rotate();
        }

        private void Rotate() {
            transform.Rotate(0f, 0f, -90);
            
            if (_currentDirection >= 4) _currentDirection = 1;
            else _currentDirection++;
        }

        private void ResetSprite() {
            _spriteRenderer.sprite = _whiteSlantSprite;
        }

        public int GetDirection() {
            return _currentDirection;
        }
    }
}