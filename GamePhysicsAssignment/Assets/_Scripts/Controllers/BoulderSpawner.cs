using UnityEngine;

namespace Controllers {
    public class BoulderSpawner : MonoBehaviour {

        // Components/Game Objects
        [SerializeField] private GameObject boulder;
        [SerializeField] private Transform spawnPoint;

        // Variables
        [SerializeField] private float spawnCoolDown = 1f;
        private float _spawnCoolDownFixed;
        private bool _spawnCoolDownTimer = false;

        private bool _canSpawn = false;
        private bool _spawn = false;

        // Initialization
        private void Start() {
            _canSpawn = true;
            _spawn = true;
            _spawnCoolDownFixed = spawnCoolDown;
        }

        private void Update() {
            if (!_canSpawn) return;
            
            // Spawns an instance of the boulder
            if (_spawn) {
                Instantiate(boulder, spawnPoint.position, Quaternion.identity);
                _spawn = false;
                _spawnCoolDownTimer = true;
            }
            
            // Spawn Cool Down
            if (_spawnCoolDownTimer) {
                _spawnCoolDownFixed -= Time.deltaTime;
                if (_spawnCoolDownFixed <= 0f) {
                    _spawnCoolDownTimer = false;
                    _spawn = true;
                    _spawnCoolDownFixed = spawnCoolDown;
                }
            }
        }
        
        /// <summary>
        /// Property for _canSpawn.
        /// </summary>
        public bool CanSpawn { set => _canSpawn = value; }
    }
}