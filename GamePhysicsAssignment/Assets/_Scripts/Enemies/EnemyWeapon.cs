using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies {
    public class EnemyWeapon : MonoBehaviour {

        [SerializeField] private Transform shootPoint;
        [SerializeField] private GameObject defaultBullet;
        private void Start() { }

        private void Update() { }

        public void Shoot(GameObject bullet = null) {
            if (bullet == null) {
                Instantiate(defaultBullet, shootPoint.position, shootPoint.rotation);
            }
            else {
                Instantiate(bullet, shootPoint.position, shootPoint.rotation);
            }
        }
    }
}