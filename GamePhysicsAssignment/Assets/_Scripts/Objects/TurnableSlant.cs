using UnityEngine;

namespace Objects {
    public class TurnableSlant : MonoBehaviour {

        private SpriteRenderer _spriteRenderer;
        
        private Sprite _whiteSlantSprite;
        [SerializeField] private Sprite redSlantSprite;


        // Initialization
        private void Start() {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _whiteSlantSprite = _spriteRenderer.sprite;
        }
        
        /// <summary>
        /// Called when either the player hits the slant or the ball bounces off the slant.
        /// </summary>
        /// <param name="rotate">Whether to rotate the slant or not. Rotates only when the player hits it.</param>
        public void Hit(bool rotate = false) {
            _spriteRenderer.sprite = redSlantSprite;
            Invoke(nameof(ResetSprite), 0.2f);
            
            if (rotate)
                Rotate();
        }

        /// <summary>
        /// Rotates the slant by -90 degrees when the player hits it.
        /// </summary>
        private void Rotate() {
            transform.Rotate(0f, 0f, -90);
        }

        /// <summary>
        /// Resets the sprite of the slant back to the default.
        /// </summary>
        private void ResetSprite() {
            _spriteRenderer.sprite = _whiteSlantSprite;
        }

    }
}