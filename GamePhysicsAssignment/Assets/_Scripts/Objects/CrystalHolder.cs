using Players;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Objects {
    public class CrystalHolder : MonoBehaviour {

        // Components/Game Objects
        private Player _player;
        private Animator _animator;
        [SerializeField] private GameObject crystalInHolder;

        // Variables
        private float _timerLength = 3f;
        private bool _timer = false;

        // Initialization
        private void Start() {
            _player = FindObjectOfType(typeof(Player)) as Player;
            _animator = GetComponent<Animator>();
            crystalInHolder.SetActive(false);
        }

        private void Update() {
            // Loads the next scene after the timer has completed
            if (_timer) {
                _timerLength -= Time.deltaTime;
                if (_timerLength <= 0f) {
                    _timer = false;
                    _timerLength = 3f;
                    LoadNextScene();
                }
            }
        }

        // Collision Checking with the Player
        private void OnTriggerEnter2D(Collider2D other) {
            if (other.gameObject.CompareTag("Player")) {
                if (_player.HasCrystal) {
                    _animator.enabled = false;
                    crystalInHolder.SetActive(true);
                    _player.HasCrystal = false;
                    
                    _timer = true;
                }
            }
        }

        /// <summary>
        /// Checks which scene is currently in play and loads the next scene.
        /// </summary>
        private void LoadNextScene() {
            if (SceneManager.GetActiveScene().buildIndex == 1)
                SceneManager.LoadScene("Robot");
            else if (SceneManager.GetActiveScene().buildIndex == 2)
                SceneManager.LoadScene("GameOverWin");
        }
    }
}