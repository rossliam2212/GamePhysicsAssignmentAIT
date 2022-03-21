using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers {
    public class BoulderSpawner : MonoBehaviour {

        [SerializeField] private GameObject boulder;
        [SerializeField] private Transform spawnPoint;
        private float _spawnCoolDown = 1f;
        private bool _spawnCoolDownTimer = false;

        private bool _canSpawn = false;
        private bool _spawn = false;

        private void Start() {
            _canSpawn = true;
            _spawn = true;
        }

        private void Update() {
            if (!_canSpawn) return;
            
            if (_spawn) {
                Instantiate(boulder, spawnPoint.position, Quaternion.identity);
                _spawn = false;
                _spawnCoolDownTimer = true;
            }
            
            if (_spawnCoolDownTimer) {
                _spawnCoolDown -= Time.deltaTime;
                if (_spawnCoolDown <= 0f) {
                    _spawnCoolDownTimer = false;
                    _spawn = true;
                    _spawnCoolDown = 1f;
                }
            }
        }
        
        public bool CanSpawn { set => _canSpawn = value; }
    }
}