using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers {
    public class BoulderSpawner : MonoBehaviour {

        [SerializeField] private GameObject boulder;
        [SerializeField] private Transform spawnPoint;
        private float _spawnCoolDown = 2f;
        private bool spawnCoolDownTimer = false;
        private bool canSpawn = false;

        private void Start() { canSpawn = true; }

        private void Update() {
            if (canSpawn) {
                Instantiate(boulder, spawnPoint.position, Quaternion.identity);
                canSpawn = false;
                spawnCoolDownTimer = true;
            }
            
            if (spawnCoolDownTimer) {
                _spawnCoolDown -= Time.deltaTime;
                if (_spawnCoolDown <= 0f) {
                    spawnCoolDownTimer = false;
                    canSpawn = true;
                    _spawnCoolDown = 2f;
                }
            }
        }
    }
}