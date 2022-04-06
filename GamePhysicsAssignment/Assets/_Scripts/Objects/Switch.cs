using Players;
using UnityEngine;
using UnityEngine.Events;

namespace Objects {
    public class Switch : MonoBehaviour {

        // Switch Event
        [SerializeField] private UnityEvent onFlip;
        
        // Components/Game Objects
        private Player _player;
        private SpriteRenderer _spriteRenderer;
        private Sprite _switchUp;
        [SerializeField] private Sprite switchFlippedLeft;
        [SerializeField] private Sprite switchFlippedRight;
        
        // Bool Flags
        [SerializeField] private bool isFlipped = false;

        // Initialization
        private void Start() {
            _player = FindObjectOfType(typeof(Player)) as Player;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _switchUp = _spriteRenderer.sprite;
        }

        /// <summary>
        /// Flips the switch in the direction the player is facing and invokes the switch UnityEvent.
        /// </summary>
        public void FlipSwitch() {
            if (isFlipped) return;

            isFlipped = true;
            onFlip.Invoke();
            
            if (_player.GetIsFacingRight())
                _spriteRenderer.sprite = switchFlippedRight;
            else
                _spriteRenderer.sprite = switchFlippedLeft;

        }
    }
}