using UnityEngine;

namespace Players {
    // Player Animation States
    public enum State {
        PlayerIdle,
        PlayerRunning,
        PlayerJumping,
        PlayerHitting,
        PlayerFlatten
    }
    
    public class PlayerAnimationManager : MonoBehaviour {

        // Components/Game Objects
        private Player _player;
        private Animator _animator;
        public State currentState;

        // Animation States
        private const string PlayerIdle = "player_idle";
        private const string PlayerRun = "player_run";
        private const string PlayerJump = "player_jump";
        private const string PlayerHit = "player_hit";
        private const string PlayerFlatten = "player_flatten";

        // Initialization
        private void Start() {
            _player = GetComponent<Player>();
            _animator = GetComponent<Animator>();

            currentState = State.PlayerJumping;
        }

        /// <summary>
        /// Changes the players current animation state.
        /// </summary>
        /// <param name="newState">The animation state to change to.</param>
        public void ChangeAnimationState(State newState) {
            if (currentState == newState) return;

            switch (newState) {
                default:
                case State.PlayerIdle:
                    _animator.Play(PlayerIdle);
                    break;
                case State.PlayerRunning:
                    _animator.Play(PlayerRun);
                    break;
                case State.PlayerJumping:
                    _animator.Play(PlayerJump);
                    break;
                case State.PlayerHitting:
                    _animator.Play(PlayerHit);
                    break;
                case State.PlayerFlatten:
                    _animator.Play(PlayerFlatten);
                    break;
            }

            currentState = newState;
        }
    }
}