using System.Collections.Generic;
using Objects;
using UnityEngine;

namespace Controllers {
    public class GlassBallEvents : MonoBehaviour {

        // Glass Ball Events for the Factory Level (Level 1)
        [SerializeField] private BoulderSpawner boulderSpawner1;
        [SerializeField] private GameObject boulderSpawnerClose1;

        [SerializeField] private List<TrapDoor> trapDoorsSet1;
        [SerializeField] private List<TrapDoor> trapDoorsSet2;
        [SerializeField] private List<TrapDoor> trapDoorsSet3;
        [SerializeField] private List<TrapDoor> trapDoorsSet4;
        [SerializeField] private List<TrapDoor> trapDoorsSet5;

        /// <summary>
        /// Closes the boulder spawner area and disables the spawner.
        /// </summary>
        public void CloseBoulderSpawner() {
            boulderSpawnerClose1.SetActive(true);
            boulderSpawner1.CanSpawn = false;
        }

        /// <summary>
        /// Flips all the trap doors in set 1 up.
        /// </summary>
        public void FlipTrapDoorsSet1() {
            foreach (var trapDoor in trapDoorsSet1) {
                trapDoor.SetTrapDoorUp();
            }
        }

        /// <summary>
        /// Flips all the trap doors in set 2 down.
        /// </summary>
        public void FlipTrapDoorSet2() {
            foreach (var trapDoor in trapDoorsSet2) {
                trapDoor.SetTrapDoorDown();
            }
        }

        /// <summary>
        /// Flips all the trap doors in set 3 up.
        /// </summary>
        public void FlipTrapDoorSet3() {
            foreach (var trapDoor in trapDoorsSet3) {
                trapDoor.SetTrapDoorUp();
            }
        }
        
        /// <summary>
        /// Flips all the trap doors in set 4 up.
        /// </summary>
        public void FlipTrapDoorSet4() {
            foreach (var trapDoor in trapDoorsSet4) {
                trapDoor.SetTrapDoorUp();
            }
        }
        
        /// <summary>
        /// Flips all the trap doors in set 5 up.
        /// </summary>
        public void FlipTrapDoorSet5() {
            foreach (var trapDoor in trapDoorsSet5) {
                trapDoor.SetTrapDoorUp();
            }
        }
    }
}