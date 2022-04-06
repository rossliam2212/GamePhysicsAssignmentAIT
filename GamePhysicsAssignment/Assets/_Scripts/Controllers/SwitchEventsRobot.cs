using UnityEngine;

namespace Controllers {
    public class SwitchEventsRobot : MonoBehaviour {

        // Switch Events for the Robot Level (Level 2)
        [SerializeField] private BoulderSpawner boulderSpawner1;
        [SerializeField] private GameObject trapDoorStartArea;

        /// <summary>
        /// Closes the start area and turns off the boulder spawner in Level 2.
        /// </summary>
        public void CloseStartArea() {
            boulderSpawner1.CanSpawn = false;
            trapDoorStartArea.SetActive(true);
        }
    }
}