using System.Collections.Generic;
using Objects;
using UnityEngine;

namespace Controllers {
    public class RedDotBlockEventsRobot : MonoBehaviour {
        
        // Red Dot Block event for the Robot Level (Level 2)
        [SerializeField] private List<TrapDoor> trapDoorsSet3;

        [SerializeField] private List<RedDotBlock> redDotBlocksSet1;
        private bool _redDotBlockSet1AllFilled = false;
        private int _redDotBlockSet1Counter = 0;

        private void Update() {
            if (!_redDotBlockSet1AllFilled) {
                // Checking if all the red dot blocks have been hit by the ball
                foreach (var block in redDotBlocksSet1) {
                    if (block.IsFilledRed) 
                        _redDotBlockSet1Counter++;
                    else {
                        _redDotBlockSet1Counter = 0;
                        break;
                    }
                }

                if (_redDotBlockSet1Counter == redDotBlocksSet1.Count) {
                    _redDotBlockSet1AllFilled = true;
                    FlipTrapDoorsSet3();
                }
            }
        }

        /// <summary>
        /// Flips all the trap doors in set 3 up.
        /// </summary>
        private void FlipTrapDoorsSet3() {
            foreach (var trapDoor in trapDoorsSet3) {
                trapDoor.SetTrapDoorUp();
            }
        }
    }
}