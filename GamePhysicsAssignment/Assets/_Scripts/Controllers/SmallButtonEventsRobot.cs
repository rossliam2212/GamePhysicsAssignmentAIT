using System.Collections.Generic;
using UnityEngine;

namespace Controllers {
    public class SmallButtonEventsRobot : MonoBehaviour {

        // Small Button Events for the Robot Level (Level 2)
        [SerializeField] private List<GameObject> orangeForceFieldsSet2;
        [SerializeField] private List<GameObject> blueForceFieldsSet1;
        [SerializeField] private List<GameObject> blueForceFieldsSet2;

        /// <summary>
        /// Disables all orange force fields in set 2.
        /// </summary>
        public void TurnOffOrangeFfSet2() {
            foreach (var forceField in orangeForceFieldsSet2) {
                forceField.SetActive(false);
            }
        }
        
        /// <summary>
        /// Disables all blue force fields in set 1.
        /// </summary>
        public void TurnOffBlueFfSet1() {
            foreach (var forceField in blueForceFieldsSet1) {
                forceField.SetActive(false);
            }
        }
        
        /// <summary>
        /// Disables all blue force fields in set 2.
        /// </summary>
        public void TurnOffBlueFfSet2() {
            foreach (var forceField in blueForceFieldsSet2) {
                forceField.SetActive(false);
            }
        }
    }
}