using System.Collections;
using System.Collections.Generic;
using Players;
using UnityEngine;

namespace Objects {
    public class Switch : MonoBehaviour {

        // Components/Game Objects
        private Player _player;
        private SpriteRenderer _spriteRenderer;
        private Sprite _switchUp;
        [SerializeField] private Sprite switchFlippedLeft;
        [SerializeField] private Sprite switchFlippedRight;
        
        // Bool Flags
        [SerializeField] private bool isFlipped = false;

        private void Start() {
            _player = FindObjectOfType(typeof(Player)) as Player;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _switchUp = _spriteRenderer.sprite;
        }

        private void Update() { }

        public void FlipSwitch() {
            if (isFlipped) return;

            isFlipped = true;
            if (_player.GetIsFacingRight())
                _spriteRenderer.sprite = switchFlippedRight;
            else
                _spriteRenderer.sprite = switchFlippedLeft;

        }
    }
}