using UnityEngine;

namespace Enemies {
    public class EnemyWeapon : MonoBehaviour {

        [SerializeField] private Transform shootPoint;
        [SerializeField] private GameObject defaultBullet;

        /// <summary>
        /// Shoots an instance of the enemy bullet in the direction they are facing.
        /// </summary>
        /// <param name="bullet">The bullet to shoot. The default enemy bullet will be used if no specific bullet is passed in.</param>
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