using UnityEngine;
using Players;
using UnityEngine.UI;

namespace Controllers {
    public class GameUIController : MonoBehaviour {

        private Player _player;

        [SerializeField] private GameObject gameOverUI;
        [SerializeField] private GameObject gameHUD;
        
        [Space]

        [SerializeField] private Image heart1;
        [SerializeField] private Image heart2;
        [SerializeField] private Image heart3;
        [SerializeField] private GameObject ball;
        [SerializeField] private GameObject crystal;

        [Space]
        
        [SerializeField] private Sprite fullHeart;
        [SerializeField] private Sprite halfHeart;
        [SerializeField] private Sprite emptyHeart;
        [SerializeField] private Sprite ballSprite;
        
        // Initialization
        private void Start() {
            _player = FindObjectOfType(typeof(Player)) as Player;
            gameOverUI.SetActive(false);
            gameHUD.SetActive(true);
        }

        private void Update() {
            CheckPlayerLives();
            ball.SetActive(_player.CanSpawnBall);
            crystal.SetActive(_player.HasCrystal);

            if (_player.Lives <= 0) {
                SetGameOverUI();
            }
        }

        /// <summary>
        /// Checks the players lives and keeps the hearts in the HUD updated.
        /// </summary>
        private void CheckPlayerLives() {
            switch (_player.Lives) {
                case 3:
                    heart1.sprite = fullHeart;
                    heart2.sprite = fullHeart;
                    heart3.sprite = fullHeart;
                    break;
                
                case 2:
                    heart1.sprite = fullHeart;
                    heart2.sprite = fullHeart;
                    heart3.sprite = emptyHeart;
                    break;
                
                case 1:
                    heart1.sprite = fullHeart;
                    heart2.sprite = emptyHeart;
                    heart3.sprite = emptyHeart;
                    break;
                
                case 0:
                    heart1.sprite = emptyHeart;
                    heart2.sprite = emptyHeart;
                    heart3.sprite = emptyHeart;
                    break;
            }
        }

        /// <summary>
        /// Sets the Game Over UI to active when the player has run out of lives.
        /// </summary>
        public void SetGameOverUI() {
            gameOverUI.SetActive(true);
            gameHUD.SetActive(false);
        }
    }
}