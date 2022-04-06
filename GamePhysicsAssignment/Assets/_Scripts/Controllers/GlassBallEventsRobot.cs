using System.Collections.Generic;
using Objects;
using UnityEngine;

namespace Controllers {
    public class GlassBallEventsRobot : MonoBehaviour {

        // Glass Ball Events for the Robot Level (Level 2)
        [SerializeField] private List<TrapDoor> trapDoorSet1;
        [SerializeField] private List<TrapDoor> trapDoorSet2;

        [SerializeField] private List<GameObject> blueForceFieldSet3;

        /// <summary>
        /// Flips all trap doors in set 1 up.
        /// </summary>
        public void FlipTrapDoorsSet1() {
            foreach (var trapDoor in trapDoorSet1) {
                trapDoor.SetTrapDoorUp();
            }
        }
        
        /// <summary>
        /// Flips all trap doors in set 2 up.
        /// </summary>
        public void FlipTrapDoorsSet2() {
            foreach (var trapDoor in trapDoorSet2) {
                trapDoor.SetTrapDoorUp();
            }
        }
        
        /// <summary>
        /// Disables all blue force fields in set 3.
        /// </summary>
        public void TurnOffBlueFfSet3() {
            foreach (var forceField in blueForceFieldSet3) {
                forceField.SetActive(false);
            }
        }
    }
}