using System.Collections;
using System.Collections.Generic;
using Objects;
using UnityEngine;

namespace Controllers {
    public class GlassBallEvents : MonoBehaviour {

        [SerializeField] private BoulderSpawner boulderSpawner1;
        [SerializeField] private GameObject boulderSpawnerClose1;

        [SerializeField] private List<TrapDoor> trapDoorsSet1;
        [SerializeField] private List<TrapDoor> trapDoorSet2;

        private void Start() { }

        private void Update() { }

        public void CloseBoulderSpawner() {
            boulderSpawnerClose1.SetActive(true);
            boulderSpawner1.CanSpawn = false;
        }

        public void FlipTrapDoorsSet1() {
            foreach (var trapDoor in trapDoorsSet1) {
                trapDoor.SetTrapDoorUp();
            }
        }

        public void FlipTrapDoorSet2() {
            foreach (var trapDoor in trapDoorSet2) {
                trapDoor.SetTrapDoorDown();
            }
        }
    }
}